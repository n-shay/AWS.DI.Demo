namespace TRG.Extensions.Configuration
{
    using System;

    public class ConfigurationException : Exception
    {
        public string SettingName { get; }

        public ConfigurationException(string settingName, Exception exception = null) :
            this("Unable to retrieve application setting '{0}'.", settingName, exception)
        {
            this.SettingName = settingName;
        }

        public ConfigurationException(string message, string settingName, Exception exception = null) :
            base(string.Format(message, settingName), exception)
        {
            this.SettingName = settingName;
        }
    }
}