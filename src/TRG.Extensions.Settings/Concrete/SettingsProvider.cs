using System;
using Microsoft.Extensions.Configuration;

namespace TRG.Extensions.Settings.Concrete
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IConfigurationProvider _configurationProvider;

        private const string SettingsSection = "Settings";

        public SettingsProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            try
            {
                return _configurationProvider.Get()
                    .GetSection(SettingsSection)
                    .GetValue(key, defaultValue);
            }
            catch (Exception ex)
            {
                throw new SettingException(key, ex);
            }
        }

        public string GetConnectionString(string name)
        {
            return _configurationProvider.Get()
                .GetConnectionString(name);
        }
    }
}