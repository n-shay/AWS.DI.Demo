namespace TRG.Extensions.Settings
{
    using System;
    using System.Collections.Generic;

    using TRG.Extensions.Configuration;

    public class SettingsProvider : ISettingsProvider
    {
        private readonly IConfigurationProvider configurationProvider;

        private readonly object syncRoot = new object();

        private readonly IDictionary<Type, Delegate>
            configDictionary = new Dictionary<Type, Delegate>();

        public SettingsProvider(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        public ISettings<T> Get<T>()
        {
            return new Settings<T>(this.CreateInternal<T>); 
        }

        public void Configure<T>(Func<IConfigurationProvider, T> config)
        {
            var type = typeof(T);
            lock (this.syncRoot)
            {
                if (this.configDictionary.ContainsKey(type))
                    this.configDictionary[type] = config;
                else
                    this.configDictionary.Add(type, config);
            }
        }

        private T CreateInternal<T>()
        {
            Func<IConfigurationProvider, T> func;
            lock (this.syncRoot)
            {
                if(!this.configDictionary.TryGetValue(typeof(T), out var val))
                    throw new ConfigurationException("Cannot retrieve settings for type '{0}'. Provider was not configured.", typeof(T).Name);
                func = (Func<IConfigurationProvider, T>) val;
            }

            return func(this.configurationProvider);
        }
    }
}