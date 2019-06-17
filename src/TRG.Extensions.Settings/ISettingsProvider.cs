namespace TRG.Extensions.Settings
{
    public interface ISettingsProvider
    {
        T Get<T>(string key, T defaultValue = default(T));

        string GetConnectionString(string name);
    }
}