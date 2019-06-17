using Serilog;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Settings;

namespace TRG.Extensions.Logging.Serilog
{
    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseSerilog(this IDependencyCollection collection, IConfigurationProvider configurationProvider)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationProvider.Get())
                .CreateLogger();

            collection.Add(new SerilogDependencyDescriptor());
            return collection;
        }
    }
}