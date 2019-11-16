namespace TRG.Extensions.Configuration
{
    using System;
    using System.Configuration;

    using Microsoft.Extensions.Configuration;

    public class LegacyConfigurationProvider : IConfigurationProvider
    {
        private readonly string environmentNameAppSettingKey;

        public LegacyConfigurationProvider(string environmentNameAppSettingKey = null)
        {
            this.environmentNameAppSettingKey = environmentNameAppSettingKey;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            var section = ConfigurationManager.GetSection(key);
            if (section is T result)
            {
                value = result;
                return true;
            }

            value = default;
            return false;
        }

        public T GetValue<T>(string key, T defaultValue = default)
        {
            return this.TryGetValue(key, out T val) ? val : defaultValue;
        }

        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        }

        public string GetEnvironmentName()
        {
            throw new NotSupportedException($"{nameof(this.GetEnvironmentName)} is not supported in {nameof(LegacyConfigurationProvider)}.");
        }

        public IConfigurationRoot GetRoot()
        {
            throw new NotSupportedException($"{nameof(this.GetRoot)} is not supported in {nameof(LegacyConfigurationProvider)}.");
        }
    }
}