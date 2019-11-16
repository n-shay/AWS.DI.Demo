namespace TRG.Extensions.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static Task WhenAll(this IEnumerable<Task> tasks, CancellationToken cancellationToken = default)
        {
            if (tasks == null) throw new ArgumentNullException(nameof(tasks));

            return Task.WhenAny(Task.WhenAll(tasks), cancellationToken.AsTask());
        }

        /// <summary>
        /// Creates a task that canceled when the provided <paramref name="cancellationToken"/> is canceled.
        /// </summary>
        public static Task AsTask(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>();
            cancellationToken.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
            return tcs.Task;
        }
    }
}