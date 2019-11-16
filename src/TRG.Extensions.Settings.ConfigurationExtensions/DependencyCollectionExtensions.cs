namespace TRG.Extensions.Settings.ConfigurationExtensions
{
    using System;

    using Autofac;

    using TRG.Extensions.Configuration;
    using TRG.Extensions.DependencyInjection;
    using TRG.Extensions.Settings;

    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseConfiguration(this IDependencyCollection collection, IConfigurationProvider configurationProvider)
        {
            collection.Add(new ConfigDependencyDescriptor(configurationProvider));
            return collection;
        }

        public static IDependencyCollection UseSettingsProvider(this IDependencyCollection collection, IConfigurationProvider configurationProvider)
        {
            collection.Add(new ProviderDependencyDescriptor(configurationProvider));
            return collection;
        }

        public static IDependencyCollection Configure<T>(this IDependencyCollection collection, Func<T> valueFactory)
        {
            collection.Add(new SettingsDependencyDescriptor<T>(valueFactory));
            return collection;
        }

        public static IDependencyCollection Configure<T>(this IDependencyCollection collection, T value)
        {
            collection.Add(new SettingsDependencyDescriptor<T>(() => value));
            return collection;
        }

        private class ConfigDependencyDescriptor : DependencyDescriptor
        {
            private readonly IConfigurationProvider configurationProvider;

            public ConfigDependencyDescriptor(IConfigurationProvider configurationProvider)
            {
                this.configurationProvider = configurationProvider;
            }

            protected override void Register(RegistrationBuilder builder)
            {
                builder.Include(c =>
                {
                    c.RegisterInstance(this.configurationProvider)
                        .As<IConfigurationProvider>()
                        .SingleInstance()
                        .IfNotRegistered(typeof(IConfigurationProvider));
                });
            }
        }

        private class ProviderDependencyDescriptor : DependencyDescriptor
        {
            private readonly IConfigurationProvider configurationProvider;

            public ProviderDependencyDescriptor(IConfigurationProvider configurationProvider)
            {
                this.configurationProvider = configurationProvider;
            }

            protected override void Register(RegistrationBuilder builder)
            {
                builder.Include(c =>
                {
                    c.RegisterInstance(this.configurationProvider)
                        .As<IConfigurationProvider>()
                        .SingleInstance()
                        .IfNotRegistered(typeof(IConfigurationProvider));

                    c.RegisterType<SettingsProvider>()
                        .As<ISettingsProvider>()
                        .WithParameter(TypedParameter.From(this.configurationProvider))
                        .SingleInstance();
                });
            }
        }

        private class SettingsDependencyDescriptor<T> : DependencyDescriptor
        {
            private readonly Func<T> valueFactory;

            public SettingsDependencyDescriptor(Func<T> valueFactory)
            {
                this.valueFactory = valueFactory;
            }

            protected override void Register(RegistrationBuilder builder)
            {
                builder.Include(c =>
                    {
                        c.RegisterType<Settings<T>>()
                            .As<ISettings<T>>()
                            .WithParameter(TypedParameter.From(this.valueFactory));
                    });
            }

        }
    }
}