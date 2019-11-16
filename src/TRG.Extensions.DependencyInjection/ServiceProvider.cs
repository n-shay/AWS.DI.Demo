namespace TRG.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;
    using Autofac.Core;

    public sealed class ServiceProvider : IServiceProvider, IDisposable
    {
        private readonly IContainer container;

        /// <summary>
        /// Shorthand for <code>new ServiceProvider(new DependencyCollection().UseCurrentDomain())</code>.
        /// </summary>
        public ServiceProvider()
            : this(new DependencyCollection().UseCurrentDomain())
        {
        }

        public ServiceProvider(IEnumerable<DependencyDescriptor> dependencyDescriptors)
            : this(dependencyDescriptors, ServiceProviderOptions.DEFAULT)
        {
        }

        public ServiceProvider(IEnumerable<DependencyDescriptor> dependencyDescriptors, ServiceProviderOptions options)
        {
            var containerBuilder = new ContainerBuilder();
            
            var list = (options.SupportPriority
                ? dependencyDescriptors
                : dependencyDescriptors.OrderByDescending(dd => dd.Priority)).ToList();

            if (options.SelfRegister)
            {
                list.Add(new SelfDependencyDescriptor(this));
            }

            foreach (var descriptor in list)
            {
                descriptor.Register(containerBuilder);
            }

            this.container = containerBuilder.Build();
        }

        public T Resolve<T>(params object[] arguments)
        {
            return (T)(this.Resolve(typeof(T), arguments) ?? default(T));
        }

        public object Resolve(Type serviceType, params object[] arguments)
        {
            // If no parameters then simply call Get.
            if (arguments == null || !arguments.Any())
                return this.container.Resolve(serviceType);

            try
            {
                return this.container.Resolve(
                    serviceType,
                    arguments.Select(
                        arg => new ResolvedParameter(
                            (p, c) => p.ParameterType.IsInstanceOfType(arg),
                            (p, c) => arg)));
            }
            catch (Exception ex)
            {
                throw new DependencyResolutionException(
                    $"Dependency Injection binding for {serviceType.Name} failed.",
                    ex);
            }
        }

        public void Dispose()
        {
            this.container.Dispose();
        }
    }
}