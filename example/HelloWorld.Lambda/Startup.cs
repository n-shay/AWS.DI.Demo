using Amazon.Lambda.Core;
using HelloWorld.Lambda.Business;
using HelloWorld.Lambda.Models;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Lambda;
using TRG.Extensions.Logging.Serilog.Lambda;
using TRG.Extensions.Settings;

namespace HelloWorld.Lambda
{
    public class Startup : Handler<Input, Output>
    {
        public Startup() : base(new StartupInitializer())
        {
        }
        
        public override Output Handle(Input input, ILambdaContext context)
        {
            var speakService = Context.ServiceProvider.Resolve<ISpeakService>();
            var output =  new Output
            {
                Message = speakService.SaySomething()
            };

            return output;
        }

        public class StartupInitializer : Initializer
        {
            protected override void Configure(IDependencyCollection dependencyCollection, IConfigurationProvider configurationProvider)
            {
                dependencyCollection.UseCurrentDomain()
                //dependencyCollection.UsePackage<DependencyResolution.HelloWorldPackage>()
                    .UseSerilog(configurationProvider);
            }
        }
    }
}
