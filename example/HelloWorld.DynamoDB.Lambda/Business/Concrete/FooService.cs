using TRG.Extensions.Logging;
using TRG.Extensions.Settings;

namespace HelloWorld.DynamoDB.Lambda.Business.Concrete
{
    public class FooService : IFooService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILogger _logger;

        public FooService(ISettingsProvider settingsProvider, ILogger logger)
        {
            _settingsProvider = settingsProvider;
            _logger = logger;
        }

        public string SaySomething()
        {
            _logger.Information("Starting speaking...");

            var result = _settingsProvider.GetAppSettings<Settings>();

            _logger.Information("Finished speaking!");
            
            return result;
        }
    }
}