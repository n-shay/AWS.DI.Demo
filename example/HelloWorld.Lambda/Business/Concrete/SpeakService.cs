using TRG.Extensions.Logging;
using TRG.Extensions.Settings;

namespace HelloWorld.Lambda.Business.Concrete
{
    public class SpeakService : ISpeakService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILogger _logger;

        public SpeakService(ISettingsProvider settingsProvider, ILogger logger)
        {
            _settingsProvider = settingsProvider;
            _logger = logger;
        }

        public string SaySomething()
        {
            _logger.Information("Starting speaking...");

            var result = _settingsProvider.Get(Settings.Message.HelloWorld, "N/A");

            _logger.Information("Finished speaking!");
            
            return result;
        }
    }
}