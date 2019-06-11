using System;
using Microsoft.Extensions.Configuration;

namespace AWS.DI.Application.Lambda
{
    public class LambdaSettingsProvider : ISettingsProvider
    {
        private const string SettingsSection = "Settings";

        private const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";

        private readonly ILambdaConfiguration _configuration;

        public LambdaSettingsProvider(ILambdaConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            try
            {
                return _configuration.Configuration.GetSection(SettingsSection).GetValue(key, defaultValue);
            }
            catch (Exception ex)
            {
                throw new SettingException(key, ex);
            }
        }

        public EnvironmentType GetEnvironment()
        {
            return Enum.TryParse(Environment.GetEnvironmentVariable(EnvironmentVariableName),
                out EnvironmentType environmentType)
                ? environmentType
                : EnvironmentType.Unknown;
        }

        public string GetConnectionString(string name)
        {
            return _configuration.Configuration.GetConnectionString(name);
        }
    }
}