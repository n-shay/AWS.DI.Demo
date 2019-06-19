using System;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using HelloWorld.DynamoDB.Lambda.Models;
using TRG.Extensions.Diagnosis;
using Xunit;
using Xunit.Abstractions;

namespace HelloWorld.DynamoDB.Lambda.Tests
{
    public class StartupTests
    {
        private readonly ITestOutputHelper _output;

        public StartupTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public async Task Test1()
        {
            Output response;
            var context = new TestLambdaContext();

            using (var bt = new BlockTelemeter("Test", _output.WriteLine, false))
            {
                var function = new Startup();
                bt.Snap("Startup");

                response = await function.HandleAsync(null, context);
            }

            Assert.NotNull(response?.Value);
            Assert.Equal(1, response.Value.Id);
            Assert.Equal(15, response.Value.Num);
            Assert.Equal("This is the text", response.Value.Text);

        }
    }
}
