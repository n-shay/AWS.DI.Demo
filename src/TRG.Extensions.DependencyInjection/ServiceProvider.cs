using System;
using System.Collections.Generic;
using SimpleInjector;

namespace TRG.Extensions.DependencyInjection
{
    public sealed class ServiceProvider : IServiceProvider, IDisposable
    {
        private readonly Container _container;

        public ServiceProvider(IEnumerable<DependencyDescriptor> dependencyDescriptors)
            : this(dependencyDescriptors, ServiceProviderOptions.Default)
        {
        }

        public ServiceProvider(IEnumerable<DependencyDescriptor> dependencyDescriptors, ServiceProviderOptions options)
        {
            _container = new Container();

            foreach (var descriptor in dependencyDescriptors)
            {
                descriptor.Register(_container);
            }

            if (options.VerifyOnBuild)
            {
                _container.Verify();
            }
        }

        public T Resolve<T>()
            where T : class
        {
            return _container.GetInstance<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}