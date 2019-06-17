using HelloWorld.Business;
using HelloWorld.Business.Concrete;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace HelloWorld.Lambda.DependencyResolution
{
    public class HelloWorldPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            // Business
            container.Register<ISpeakService, SpeakService>();
        }
    }
}