namespace TRG.Extensions.Configuration
{
    using Microsoft.Extensions.Configuration;

    public interface IConfigurationProvider
    {
        /// <summary>
        /// Tries to get a configuration value of type <see cref="T"/> for the specified key.
        /// </summary>
        bool TryGetValue<T>(string key, out T value);

        /// <summary>
        /// Retrieves a configuration value of type <see cref="T"/> for a specified key.
        /// <para>
        /// <see cref="defaultValue"/> is returned if key was not found or failed to convert to <see cref="T"/>.
        /// </para>
        /// </summary>
        T GetValue<T>(string key, T defaultValue = default);
        
        string GetConnectionString(string name);

        string GetEnvironmentName();

        IConfigurationRoot GetRoot();
    }
}