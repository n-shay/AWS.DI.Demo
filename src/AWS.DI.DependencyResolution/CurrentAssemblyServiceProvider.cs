using System.Linq;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace AWS.DI.DependencyResolution
{
    public class CurrentAssemblyServiceProvider : IServiceProvider
    {
        private readonly Container _container;

        public CurrentAssemblyServiceProvider()
        {
            _container = new Container();

            //var types = this.GetType().Assembly.GetTypes()
            //    .Where(t => typeof(IPackage).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            //    .ToList();

            _container.RegisterPackages(new[] {this.GetType().Assembly});

            _container.Verify();
        }
        public T Resolve<T>()
            where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}