using SimpleInjector;
using SimpleInjector.Packaging;

namespace HelloWorld.DynamoDB.Lambda.DependencyResolution
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            // Business
            //container.Register<ISpeakService, SpeakService>();
        }
    }
}