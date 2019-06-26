using System;
using System.Collections.Generic;
using System.Linq;
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

            var list = (options.SupportPriority
                ? dependencyDescriptors
                : dependencyDescriptors.OrderByDescending(dd => dd.Priority)).ToList();

            if (options.SelfRegister)
            {
                list.Add(new SelfDependencyDescriptor(this));
            }

            foreach (var descriptor in list)
            {
                descriptor.Configure(_container);
            }

            foreach (var descriptor in list)
            {
                descriptor.Register(_container);
            }

            if (options.VerifyOnBuild)
            {
                _container.Verify();
            }
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}