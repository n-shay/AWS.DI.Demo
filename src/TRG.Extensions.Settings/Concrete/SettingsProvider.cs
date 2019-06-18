using System;
using Microsoft.Extensions.Configuration;

namespace TRG.Extensions.Settings.Concrete
{
    public class SettingsProvider : ISettingsProvider
    {
        private const string SettingsSection = "Settings";

        private readonly IConfigurationProvider _configurationProvider;

        public SettingsProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public T GetValue<T>(string key, T defaultValue = default)
        {
            try
            {
                return _configurationProvider.Get()
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

        public T GetAppSettings<T>()
        {
            var settings = _configurationProvider.Get()
                .GetSection(SettingsSection)
                .Get<T>();

            return settings;
        }
    }
}