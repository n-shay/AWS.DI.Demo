using AWS.DI.Application;
using AWS.DI.Application.Lambda;
using AWS.DI.Business;
using AWS.DI.Business.Concrete;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace AWS.DI.DependencyResolution.Modules
{
    public class HelloWorldPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            // Application
            container.Register<ILambdaConfiguration, LambdaConfiguration>(Lifestyle.Singleton);
            container.Register<ISettingsProvider, LambdaSettingsProvider>();

            // Business
            container.Register<ISpeakService, SpeakService>();
        }
    }
}