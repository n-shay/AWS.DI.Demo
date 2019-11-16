namespace TRG.Extensions.Logging.Serilog.Lambda
{
    using TRG.Extensions.Configuration;
    using TRG.Extensions.DependencyInjection;

    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseSerilog(this IDependencyCollection collection, IConfigurationProvider configurationProvider)
        {
            return Serilog.DependencyCollectionExtensions.UseSerilog(collection, configurationProvider);
        }

    }
}