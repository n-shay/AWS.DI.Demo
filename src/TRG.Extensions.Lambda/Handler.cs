using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Settings;
using TRG.Extensions.Settings.Lambda;

namespace TRG.Extensions.Lambda
{
    public abstract class Handler
    {
        public ExecutionContext Context { get; }

        protected Handler(Initializer initializer)
        {
            Context = initializer.Build();
        }

        public abstract class Initializer
        {
            private readonly IConfigurationProvider _configurationProvider;

            protected Initializer(IConfigurationProvider configurationProvider = null)
            {
                _configurationProvider = configurationProvider ?? ConfigurationProvider.CreateDefault();
            }

            protected abstract void Configure(IDependencyCollection dependencyCollection, IConfigurationProvider configurationProvider);

            internal ExecutionContext Build()
            {
                var collection = new DependencyCollection()
                    .SetConfigurationProvider(_configurationProvider)
                    .UseSettingProvider();
                Configure(collection, _configurationProvider);

                var serviceProvider = new ServiceProvider(collection);
                return new ExecutionContext(serviceProvider);
            }
        }
    }
}