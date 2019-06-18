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
    }
}
