using AWS.DI.Application;

namespace AWS.DI.Business.Concrete
{
    public class SpeakService : ISpeakService
    {
        private readonly ISettingsProvider _settingsProvider;

        public SpeakService(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public string SaySomething()
        {
            return _settingsProvider.Get(Settings.Message.HelloWorld, "N/A");
        }
    }
}