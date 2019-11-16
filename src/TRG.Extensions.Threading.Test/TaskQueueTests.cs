namespace TRG.Extensions.Threading.Test
{
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    public class TaskQueueTests
    {
        [Fact]
        public async Task TestMaxDegreeOfParallelism()
        {
            // Arrange
            var n = 0;
            var taskQueue = new TaskQueue(4);
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(40);
                n++;
            });
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(50);
                n++;
            });
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(60);
                n++;
            });
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(70);
                n++;
            });

            
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(40);
                n++;
            });
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(50);
                n++;
            });

            // Act & Assert
            var t = taskQueue.WhenAll();

            await Task.Delay(10);

            Assert.Equal(2, taskQueue.Waiting);
            Assert.Equal(4, taskQueue.Executing);
            Assert.Equal(0, taskQueue.Completed);

            await t;

            Assert.Equal(0, taskQueue.Waiting);
            Assert.Equal(0, taskQueue.Executing);
            Assert.Equal(6, taskQueue.Completed);
            Assert.Equal(6, n);
        }

        [Fact]
        public async Task TestCancellationTokenCancelled()
        {
            // Arrange
            var n = 0;
            var taskQueue = new TaskQueue(2);
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(40);
                n++;
            });
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(50);
                n++;
            });

            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(60);
                n++;
            });
            taskQueue.Enqueue(async () =>
            {
                await Task.Delay(70);
                n++;
            });
            var cancellationTokenSource = new CancellationTokenSource();

            // Act & Assert

            var t = taskQueue.WhenAll(cancellationTokenSource.Token);

            await Task.Delay(10);

            Assert.Equal(2, taskQueue.Waiting);
            Assert.Equal(2, taskQueue.Executing);
            Assert.Equal(0, taskQueue.Completed);

            cancellationTokenSource.Cancel();

            await t;

            Assert.True(cancellationTokenSource.IsCancellationRequested);
            Assert.True(t.IsCompleted);
            Assert.Equal(2, taskQueue.Waiting);
            Assert.Equal(2, taskQueue.Executing);
            Assert.Equal(0, taskQueue.Completed);
            Assert.Equal(0, n);
        }
    }
}
