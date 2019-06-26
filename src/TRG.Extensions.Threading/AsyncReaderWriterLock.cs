using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TRG.Extensions.Threading
{
    /// <summary>
    /// A reader/writer lock that is compatible with async.
    /// </summary>
    public sealed class AsyncReaderWriterLock
    {
        /// <summary>
        /// The queue of TCSs that other tasks are awaiting to acquire the lock as writers.
        /// </summary>
        private readonly Queue<TaskCompletionSource<IDisposable>> _writerQueue;
        
        /// <summary>
        /// The queue of TCSs that other tasks are awaiting to acquire the lock as readers.
        /// </summary>
        private readonly Queue<TaskCompletionSource<IDisposable>> _readerQueue;

        /// <summary>
        /// The object used for mutual exclusion.
        /// </summary>
        private readonly object _mutex;

        /// <summary>
        /// Number of reader locks held; -1 if a writer lock is held; 0 if no locks are held.
        /// </summary>
        private int _locksHeld;

        // Creates a new async-compatible reader/writer lock.
        public AsyncReaderWriterLock()
        {
            _writerQueue = new Queue<TaskCompletionSource<IDisposable>>();
            _readerQueue = new Queue<TaskCompletionSource<IDisposable>>();
            _mutex = new object();
        }

        /// <summary>
        /// Applies a continuation to the task that will call <see cref="ReleaseWaiters"/> if the task is canceled. This method may not be called while holding the sync lock.
        /// </summary>
        /// <param name="task">The task to observe for cancellation.</param>
        private void ReleaseWaitersWhenCanceled(Task task)
        {
            task.ContinueWith(t =>
            {
                lock (_mutex) { ReleaseWaiters(); }
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        // Asynchronously acquires the lock as a writer.
        // Returns a disposable that releases the lock when disposed.
        public Task<IDisposable> WriterLockAsync()
        {
            Task<IDisposable> result;
            lock (_mutex)
            {
                // If the lock is available, take it immediately.
                if (_locksHeld == 0)
                {
                    _locksHeld = -1;
                    result = Task.FromResult<IDisposable>(new WriterKey(this));
                }
                else
                {
                    // Wait for the lock to become available
                    var tcs = new TaskCompletionSource<IDisposable>(TaskCreationOptions.RunContinuationsAsynchronously);
                    _writerQueue.Enqueue(tcs);
                    result = tcs.Task;
                }
            }

            ReleaseWaitersWhenCanceled(result);
            return result;
        }

        // Asynchronously acquires the lock as a reader.
        // Returns a disposable that releases the lock when disposed.
        public Task<IDisposable> ReaderLockAsync()
        {
            lock (_mutex)
            {
                // If the lock is available or in read mode and there are no waiting writers, upgradeable readers, or upgrading readers, take it immediately.
                if (_locksHeld >= 0 && _writerQueue.Count == 0)
                {
                    ++_locksHeld;
                    return Task.FromResult<IDisposable>(new ReaderKey(this));
                }
                else
                {
                    // Wait for the lock to become available
                    var tcs = new TaskCompletionSource<IDisposable>(TaskCreationOptions.RunContinuationsAsynchronously);
                    _readerQueue.Enqueue(tcs);
                    return tcs.Task;
                }
            }
        }

        /// <summary>
        /// Releases the lock as a reader.
        /// </summary>
        private void ReleaseReaderLock()
        {
            lock (_mutex)
            {
                --_locksHeld;
                ReleaseWaiters();
            }
        }

        /// <summary>
        /// Releases the lock as a writer.
        /// </summary>
        private void ReleaseWriterLock()
        {
            lock (_mutex)
            {
                _locksHeld = 0;
                ReleaseWaiters();
            }
        }

        /// <summary>
        /// Grants lock(s) to waiting tasks. This method assumes the sync lock is already held.
        /// </summary>
        private void ReleaseWaiters()
        {
            if (_locksHeld != 0)
                return;

            // Give priority to writers.
            if (_writerQueue.Count > 0)
            {
                _locksHeld = -1;
                var tcs = _writerQueue.Dequeue();
                tcs.TrySetResult(new WriterKey(this));
                return;
            }

            // Then to readers.
            while (_readerQueue.Count > 0)
            {
                var tcs = _readerQueue.Dequeue();
                tcs.TrySetResult(new ReaderKey(this));
                ++_locksHeld;
            }
        }

        /// <summary>
        /// The disposable which releases the reader lock.
        /// </summary>
        private sealed class ReaderKey : IDisposable
        {
            private readonly AsyncReaderWriterLock _lock;

            public ReaderKey(AsyncReaderWriterLock @lock)
            {
                _lock = @lock;
            }

            // Release the lock.
            public void Dispose()
            {
                if(_lock != null)
                {
                    _lock.ReleaseReaderLock();
                }
            }
        }

        /// <summary>
        /// The disposable which releases the writer lock.
        /// </summary>
        private sealed class WriterKey : IDisposable
        {
            private readonly AsyncReaderWriterLock _lock;

            public WriterKey(AsyncReaderWriterLock @lock)
            {
                _lock = @lock;
            }

            // Release the lock.
            public void Dispose()
            {
                if(_lock != null)
                {
                    _lock.ReleaseWriterLock();
                }
            }
        }
    }
}
