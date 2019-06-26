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
        public async Task SearchFunctionTest()
        {
            var searchInput = new SearchInput {Num = 15, Text = "is"};

            MultiItemOutput response;
            var context = new TestLambdaContext();

            using (var bt = new BlockTelemeter("Test", _output.WriteLine, false))
            {
                var function = new SearchFunction();
                bt.Snap("Startup");

                response = await function.HandleAsync(searchInput, context);
            }

            Assert.NotNull(response?.Value);
            Assert.NotEmpty(response.Value);

        }
    }
}
