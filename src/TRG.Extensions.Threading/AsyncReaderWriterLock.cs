namespace TRG.Extensions.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A reader/writer lock that is compatible with async.
    /// </summary>
    public sealed class AsyncReaderWriterLock
    {
        /// <summary>
        /// The queue of TCSs that other tasks are awaiting to acquire the lock as writers.
        /// </summary>
        private readonly Queue<TaskCompletionSource<IDisposable>> writerQueue;
        
        /// <summary>
        /// The queue of TCSs that other tasks are awaiting to acquire the lock as readers.
        /// </summary>
        private readonly Queue<TaskCompletionSource<IDisposable>> readerQueue;

        /// <summary>
        /// The object used for mutual exclusion.
        /// </summary>
        private readonly object mutex;

        /// <summary>
        /// Number of reader locks held; -1 if a writer lock is held; 0 if no locks are held.
        /// </summary>
        private int locksHeld;

        // Creates a new async-compatible reader/writer lock.
        public AsyncReaderWriterLock()
        {
            this.writerQueue = new Queue<TaskCompletionSource<IDisposable>>();
            this.readerQueue = new Queue<TaskCompletionSource<IDisposable>>();
            this.mutex = new object();
        }

        /// <summary>
        /// Applies a continuation to the task that will call <see cref="ReleaseWaiters"/> if the task is canceled. This method may not be called while holding the sync lock.
        /// </summary>
        /// <param name="task">The task to observe for cancellation.</param>
        private void ReleaseWaitersWhenCanceled(Task task)
        {
            task.ContinueWith(t =>
            {
                lock (this.mutex) { this.ReleaseWaiters(); }
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        // Asynchronously acquires the lock as a writer.
        // Returns a disposable that releases the lock when disposed.
        public Task<IDisposable> WriterLockAsync()
        {
            Task<IDisposable> result;
            lock (this.mutex)
            {
                // If the lock is available, take it immediately.
                if (this.locksHeld == 0)
                {
                    this.locksHeld = -1;
                    result = Task.FromResult<IDisposable>(new WriterKey(this));
                }
                else
                {
                    // Wait for the lock to become available
                    var tcs = new TaskCompletionSource<IDisposable>(TaskCreationOptions.RunContinuationsAsynchronously);
                    this.writerQueue.Enqueue(tcs);
                    result = tcs.Task;
                }
            }

            this.ReleaseWaitersWhenCanceled(result);
            return result;
        }

        // Asynchronously acquires the lock as a reader.
        // Returns a disposable that releases the lock when disposed.
        public Task<IDisposable> ReaderLockAsync()
        {
            lock (this.mutex)
            {
                // If the lock is available or in read mode and there are no waiting writers, upgradeable readers, or upgrading readers, take it immediately.
                if (this.locksHeld >= 0 && this.writerQueue.Count == 0)
                {
                    ++this.locksHeld;
                    return Task.FromResult<IDisposable>(new ReaderKey(this));
                }
                else
                {
                    // Wait for the lock to become available
                    var tcs = new TaskCompletionSource<IDisposable>(TaskCreationOptions.RunContinuationsAsynchronously);
                    this.readerQueue.Enqueue(tcs);
                    return tcs.Task;
                }
            }
        }

        /// <summary>
        /// Releases the lock as a reader.
        /// </summary>
        private void ReleaseReaderLock()
        {
            lock (this.mutex)
            {
                --this.locksHeld;
                this.ReleaseWaiters();
            }
        }

        /// <summary>
        /// Releases the lock as a writer.
        /// </summary>
        private void ReleaseWriterLock()
        {
            lock (this.mutex)
            {
                this.locksHeld = 0;
                this.ReleaseWaiters();
            }
        }

        /// <summary>
        /// Grants lock(s) to waiting tasks. This method assumes the sync lock is already held.
        /// </summary>
        private void ReleaseWaiters()
        {
            if (this.locksHeld != 0)
                return;

            // Give priority to writers.
            if (this.writerQueue.Count > 0)
            {
                this.locksHeld = -1;
                var tcs = this.writerQueue.Dequeue();
                tcs.TrySetResult(new WriterKey(this));
                return;
            }

            // Then to readers.
            while (this.readerQueue.Count > 0)
            {
                var tcs = this.readerQueue.Dequeue();
                tcs.TrySetResult(new ReaderKey(this));
                ++this.locksHeld;
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
                this._lock = @lock;
            }

            // Release the lock.
            public void Dispose()
            {
                if(this._lock != null)
                {
                    this._lock.ReleaseReaderLock();
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
                this._lock = @lock;
            }

            // Release the lock.
            public void Dispose()
            {
                if(this._lock != null)
                {
                    this._lock.ReleaseWriterLock();
                }
            }
        }
    }
}
