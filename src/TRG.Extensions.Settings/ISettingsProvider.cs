namespace TRG.Extensions.Settings
{
    public interface ISettingsProvider
    {
        T GetValue<T>(string key, T defaultValue = default(T));

        string GetConnectionString(string name);

        T GetAppSettings<T>();
    }
}