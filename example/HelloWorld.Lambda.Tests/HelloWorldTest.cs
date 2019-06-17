using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using HelloWorld.Lambda;
using Xunit;
using Xunit.Abstractions;

namespace AWS.DI.Lambda.Tests
{
    public class HelloWorldTest
    {
        private readonly ITestOutputHelper _output;

        public HelloWorldTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void InstantiatedAndExecutedOnce_Success()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Startup();
            var context = new TestLambdaContext();
            var response = function.Handle(null, context);

            Assert.NotNull(response);
            Assert.Equal("Hello World!", response.Message);
        }

        [Fact]
        public async Task ColdStartTesting()
        {
            const int executionCount = 100;

            var tasks = Enumerable.Range(0, executionCount)
                .Select(i => Task.Run(() =>
                {
                    var result = InvokeHandler();

                    _output.WriteLine($"[{i}] Executed in {result:N0}ms");

                    return result;
                }))
                .ToList();

            var results = await Task.WhenAll(tasks);

            if(results.Length > 0)
                _output.WriteLine($"Average execution: {results.Average():N0}ms");
        }

        private static long InvokeHandler()
        {
            var context = new TestLambdaContext();

            var sw = Stopwatch.StartNew();

            var function = new Startup();
            function.Handle(null, context);

            sw.Stop();
            
            return sw.ElapsedMilliseconds;
        }
    }
}
