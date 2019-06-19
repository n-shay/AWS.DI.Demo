using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using HelloWorld.DynamoDB.Lambda.Domain.Models;
using HelloWorld.DynamoDB.Lambda.Models;
using Newtonsoft.Json;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Lambda;
using TRG.Extensions.Logging.Serilog.Lambda;
using TRG.Extensions.Settings;

namespace HelloWorld.DynamoDB.Lambda
{
    public class Startup : AsyncHandler<Input, Output>
    {
        public Startup() : base(new StartupInitializer())
        {
        }

        public override async Task<Output> HandleAsync(Input input, ILambdaContext context)
        {
            SomeFoo foo;
            using (IAmazonDynamoDB client = new AmazonDynamoDBClient())
            {
                using (var dbContext = new DynamoDBContext(client))
                {
                    foo = await dbContext.LoadAsync<SomeFoo>(input.Id);
                }
            }

            var data = JsonConvert.SerializeObject(foo);
            context.Logger.Log(data);

            return new Output(foo);
        }

        public class StartupInitializer : Initializer
        {
            protected override void Configure(IDependencyCollection dependencyCollection, IConfigurationProvider configurationProvider)
            {
                dependencyCollection.UseCurrentDomain()
                    .UseSerilog(configurationProvider);
            }
        }
    }
}