namespace TRG.Extensions.Logging.Serilog
{
    using global::Serilog;

    using TRG.Extensions.Configuration;
    using TRG.Extensions.DependencyInjection;

    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseSerilog(this IDependencyCollection collection, IConfigurationProvider configurationProvider)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationProvider.GetRoot())
                .CreateLogger();

            collection.Add<SerilogDependencyDescriptor>();
            return collection;
        }
    }
}