namespace TRG.Extensions.Configuration
{
    public class ConfigurationProviderOptions
    {
        /// <summary>
        /// Manually configures the environment name.
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets whether to retrieve the environment name from 'ASPNETCORE_ENVIRONMENT' environment variable.
        /// </summary>
        public bool UseNetCoreEnvironmentVariable { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to include <value>appsettings.json</value> and its environment specific version as sources.
        /// <para>Enabled by default.</para>
        /// </summary>
        public bool UseJsonFile { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to include environment variables as sources.
        /// <para>Enabled by default.</para>
        /// </summary>
        public bool IncludeEnvironmentVariables { get; set; } = true;
    }
}