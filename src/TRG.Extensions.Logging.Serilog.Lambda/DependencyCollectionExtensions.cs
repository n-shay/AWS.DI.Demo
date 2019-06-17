using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Settings;

namespace TRG.Extensions.Logging.Serilog.Lambda
{
    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseSerilog(this IDependencyCollection collection, IConfigurationProvider configurationProvider)
        {
            return Serilog.DependencyCollectionExtensions.UseSerilog(collection, configurationProvider);
        }

    }
}