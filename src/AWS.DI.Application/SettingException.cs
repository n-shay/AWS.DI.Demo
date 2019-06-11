using System;
using System.Runtime.Serialization;

namespace AWS.DI.Application
{
    [Serializable]
    public class SettingException : Exception
    {
        public string SettingName { get; }

        public SettingException(string settingName, Exception exception = null) :
            this("Unable to retrieve application setting '{0}'.", settingName, exception)
        {
            this.SettingName = settingName;
        }

        public SettingException(string message, string settingName, Exception exception = null) :
            base(string.Format(message, settingName), exception)
        {
            this.SettingName = settingName;
        }
    }
}