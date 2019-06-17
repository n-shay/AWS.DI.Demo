using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TRG.Extensions.Settings.Lambda
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public static ConfigurationProvider CreateDefault() => new ConfigurationProvider();

        private const string AppSettingsFileName = "appsettings.json";
        private const string AppSettingsEnvironmentSpecificFileNameTemplate = "appsettings.{0}.json";
        private const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";

        private readonly bool _useAppSettingsFile;
        private readonly bool _includeEnvironmentVariables;

        private readonly object _syncRoot = new object();
        private IConfigurationRoot _configurationRoot;

        public ConfigurationProvider(bool useAppSettingsFile = true, bool includeEnvironmentVariables = true)
        {
            _useAppSettingsFile = useAppSettingsFile;
            _includeEnvironmentVariables = includeEnvironmentVariables;
        }

        public IConfigurationRoot Get()
        {
            if(_configurationRoot == null)
                lock (_syncRoot)
                {
                    if (_configurationRoot == null)
                    {
                        _configurationRoot = CreateInternal();
                    }
                }

            return _configurationRoot;
        }

        public string GetEnvironmentName()
        {
            return GetEnvironmentNameInternal();
        }

        public bool IsProduction()
        {
            return GetEnvironmentTypeInternal() == EnvironmentType.Production;
        }

        public bool IsStaging()
        {
            return GetEnvironmentTypeInternal() == EnvironmentType.Staging;
        }

        public bool IsDevelopment()
        {
            return GetEnvironmentTypeInternal() == EnvironmentType.Development;
        }

        private static string GetEnvironmentNameInternal()
        {
            return Environment.GetEnvironmentVariable(EnvironmentVariableName);
        }

        private static EnvironmentType GetEnvironmentTypeInternal()
        {
            return !Enum.TryParse(GetEnvironmentNameInternal(),
                out EnvironmentType environmentType)
                ? EnvironmentType.Unknown
                : environmentType;
        }

        private IConfigurationRoot CreateInternal()
        {
            var envSpecFileName =
                string.Format(AppSettingsEnvironmentSpecificFileNameTemplate, GetEnvironmentNameInternal());

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            if (_useAppSettingsFile)
                builder = builder
                    .AddJsonFile(AppSettingsFileName, optional: false, reloadOnChange: true)
                    .AddJsonFile(envSpecFileName, optional: true, reloadOnChange: true);

            if (_includeEnvironmentVariables)
                builder = builder
                    .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}