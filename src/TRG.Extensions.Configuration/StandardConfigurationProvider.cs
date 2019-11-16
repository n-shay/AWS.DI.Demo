namespace TRG.Extensions.Configuration
{
    using System;
    using System.IO;

    using Microsoft.Extensions.Configuration;

    public class StandardConfigurationProvider : IConfigurationProvider
    {
        private const string APP_SETTINGS_FILE_NAME = "appsettings.json";
        private const string APP_SETTINGS_ENVIRONMENT_SPECIFIC_FILE_NAME_TEMPLATE = "appsettings.{0}.json";
        private const string NET_STANDARD_ENVIRONMENT_VARIABLE_NAME = "ASPNETCORE_ENVIRONMENT";

        private readonly ConfigurationProviderOptions options;

        private readonly object syncRoot = new object();
        private IConfigurationRoot configurationRoot;

        public StandardConfigurationProvider(Action<ConfigurationProviderOptions> configOptions = null)
        {
            this.options = new ConfigurationProviderOptions();
            configOptions?.Invoke(this.options);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            try
            {
                value = this.GetOrCreateInternal().GetSection(key).Get<T>();
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }

        public T GetValue<T>(string key, T defaultValue = default)
        {
            return this.TryGetValue(key, out T val) 
                ? val 
                : defaultValue;
        }

        public string GetConnectionString(string name)
        {
            return this.GetOrCreateInternal().GetConnectionString(name);
        }

        public string GetEnvironmentName()
        {
            return this.options.UseNetCoreEnvironmentVariable 
                ? GetEnvironmentNameInternal(NET_STANDARD_ENVIRONMENT_VARIABLE_NAME) 
                : this.options.EnvironmentName;
        }

        public IConfigurationRoot GetRoot()
        {
            return this.GetOrCreateInternal();
        }

        private static string GetEnvironmentNameInternal(string variableName)
        {
            return string.IsNullOrEmpty(variableName) 
                ? null 
                : Environment.GetEnvironmentVariable(variableName);
        }

        private IConfigurationRoot GetOrCreateInternal()
        {
            if (this.configurationRoot == null)
                lock (this.syncRoot)
                {
                    if (this.configurationRoot == null)
                    {
                        this.configurationRoot = this.CreateInternal();
                    }
                }

            return this.configurationRoot;
        }

        private IConfigurationRoot CreateInternal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            if (this.options.UseJsonFile)
            {
                var envSpecFileName =
                    string.Format(APP_SETTINGS_ENVIRONMENT_SPECIFIC_FILE_NAME_TEMPLATE, this.GetEnvironmentName());

                builder = builder
                    .AddJsonFile(APP_SETTINGS_FILE_NAME, optional: false, reloadOnChange: true)
                    .AddJsonFile(envSpecFileName, optional: true, reloadOnChange: true);
            }

            if (this.options.IncludeEnvironmentVariables)
                builder = builder
                    .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}