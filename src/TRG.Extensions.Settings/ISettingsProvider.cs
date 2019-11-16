namespace TRG.Extensions.Settings
{
    using System;

    using TRG.Extensions.Configuration;

    public interface ISettingsProvider
    {
        ISettings<T> Get<T>();

        void Configure<T>(Func<IConfigurationProvider, T> config);
    }
}