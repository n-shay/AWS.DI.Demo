using System;
using System.Collections.Generic;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace TRG.Extensions.DependencyInjection
{
    public abstract class DependencyDescriptor
    {
        private readonly ICollection<Action<Container>> _registerActions = new List<Action<Container>>();

        /// <summary>
        /// The order in which the dependency descriptor will be loaded; lower number means higher priority.
        /// <para>Default is 0.</para>
        /// </summary>
        public virtual int Priority { get; } = 0;

        /// <summary>
        /// Provides a method to define the container options and configurations. DO NOT include any registrations in this method.
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void Configure(ConfigurationBuilder builder)
        {
        }

        /// <summary>
        /// Provides a method to define the container registrations
        /// </summary>
        /// <param name="builder"></param>
        protected abstract void Register(RegistrationBuilder builder);

        internal void Configure(Container container)
        {
            var builder = new ConfigurationBuilder();
            Configure(builder);

            builder.Apply(container);
        }

        internal void Register(Container container)
        {
            var builder = new RegistrationBuilder();
            Register(builder);

            builder.Apply(container);
        }

        public class ConfigurationBuilder
        {
            private readonly ICollection<Action<Container>> _actions = new List<Action<Container>>();

            // TODO: abstract SimpleInjector dependencies
            public void Include(Action<Container> registerAction)
            {
                _actions.Add(registerAction);
            }

            internal void Apply(Container container)
            {
                foreach (var action in _actions)
                {
                    action(container);
                }
            }
        }

        public class RegistrationBuilder : ConfigurationBuilder
        {
            public void Use<T>() where T : IPackage, new()
            {
                Use(Activator.CreateInstance<T>());
            }

            public void Use(IPackage package)
            {
                Include(package.RegisterServices);
            }
        }
    }
}