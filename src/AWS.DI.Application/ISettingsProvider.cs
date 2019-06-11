namespace AWS.DI.Application
{
    public interface ISettingsProvider
    {
        T Get<T>(string key, T defaultValue = default(T));

        EnvironmentType GetEnvironment();

        string GetConnectionString(string name);
    }
}