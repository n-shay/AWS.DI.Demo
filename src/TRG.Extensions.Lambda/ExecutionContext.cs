namespace TRG.Extensions.Lambda
{
    using Amazon.Lambda.Core;

    using TRG.Extensions.DependencyInjection;
    using TRG.Extensions.Configuration;

    public class ExecutionContext
    {
        public ExecutionContext(IServiceProvider serviceProvider, IConfigurationProvider configurationProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.ConfigurationProvider = configurationProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public IConfigurationProvider ConfigurationProvider { get; }

        public ILambdaContext Lambda { get; internal set; }
    }
}