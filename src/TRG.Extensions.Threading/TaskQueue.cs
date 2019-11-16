namespace TRG.Extensions.Threading
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a task queue for execution with max degree of parallelism.
    /// <para>Tasks are executed one-by-one according to the provided order.</para>
    /// </summary>
    public class TaskQueue
    {
        private readonly SemaphoreSlim batcher;
        private volatile int waiting;
        private volatile int completed;
        private volatile int executing;

        private readonly BlockingCollection<Func<Task>> taskCollection = new BlockingCollection<Func<Task>>();

        public TaskQueue(int maxDegreeOfParallelism)
        {
            this.batcher = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
        }

        public void Enqueue(Func<Task> taskFunc)
        {
            if (taskFunc == null) throw new ArgumentNullException(nameof(taskFunc));

            this.taskCollection.Add(taskFunc);
        }

        public Task WhenAll(CancellationToken cancellationToken = default)
        {
            return this.taskCollection.Select(async t => await this.DoWorkAsync(t, cancellationToken))
                .WhenAll(cancellationToken);
        }

        public int Waiting => this.waiting;

        public int Executing => this.executing;

        public int Completed => this.completed;

        public int Total => this.taskCollection.Count;

        private async Task DoWorkAsync(Func<Task> workFunc, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref this.waiting);

            await this.batcher.WaitAsync(cancellationToken);
            
            Interlocked.Decrement(ref this.waiting);
            Interlocked.Increment(ref this.executing);

            try
            {
                await Task.Run(workFunc, cancellationToken);
            }
            finally
            {
                Interlocked.Decrement(ref this.executing);

                this.batcher.Release();

                Interlocked.Increment(ref this.completed);
            }
        }

    }
}