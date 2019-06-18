using Amazon.Lambda.Core;
using HelloWorld.DynamoDB.Lambda.Models;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Lambda;
using TRG.Extensions.Logging.Serilog.Lambda;
using TRG.Extensions.Settings;

namespace HelloWorld.DynamoDB.Lambda
{
    public class Startup : Handler<Input, Output>
    {
        public Startup() : base(new StartupInitializer())
        {
        }

        public override Output Handle(Input input, ILambdaContext context)
        {
            Context.ServiceProvider.Resolve<>()
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