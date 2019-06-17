using Microsoft.Extensions.Configuration;

namespace TRG.Extensions.Settings
{
    public interface IConfigurationProvider
    {
        IConfigurationRoot Get();

        string GetEnvironmentName();

        bool IsProduction();

        bool IsStaging();

        bool IsDevelopment();
    }
}