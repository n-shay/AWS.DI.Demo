namespace TRG.Extensions.Lambda
{
    using TRG.Extensions.Configuration;
    using TRG.Extensions.DependencyInjection;
    using TRG.Extensions.Settings.ConfigurationExtensions;

    public abstract class Handler
    {
        public ExecutionContext Context { get; }

        protected Handler(Initializer initializer)
        {
            this.Context = initializer.Build();
        }

        public abstract class Initializer
        {
            private readonly IConfigurationProvider configurationProvider;

            protected Initializer(IConfigurationProvider configurationProvider = null)
            {
                this.configurationProvider = configurationProvider;
            }

            protected abstract void Configure(IDependencyCollection dependencyCollection, IConfigurationProvider configurationProvider);

            internal ExecutionContext Build()
            {
                var collection = new DependencyCollection();

                if (this.configurationProvider != null)
                    collection.UseSettingsProvider(this.configurationProvider);

                this.Configure(collection, this.configurationProvider);

                var serviceProvider = new ServiceProvider(collection);
                return new ExecutionContext(serviceProvider, this.configurationProvider);
            }
        }
    }
}