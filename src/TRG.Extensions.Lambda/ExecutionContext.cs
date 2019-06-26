using Amazon.Lambda.Core;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Settings;

namespace TRG.Extensions.Lambda
{
    public class ExecutionContext
    {
        public ExecutionContext(IServiceProvider serviceProvider, IConfigurationProvider configurationProvider)
        {
            ServiceProvider = serviceProvider;
            ConfigurationProvider = configurationProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public IConfigurationProvider ConfigurationProvider { get; }

        public ILambdaContext Lambda { get; internal set; }
    }
}