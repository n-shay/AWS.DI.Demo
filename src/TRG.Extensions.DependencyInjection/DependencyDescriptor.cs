namespace TRG.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;

    using Autofac;

    public abstract class DependencyDescriptor
    {
        /// <summary>
        /// The order in which the dependency descriptor will be loaded; lower number means higher priority.
        /// <para>Default is 9999.</para>
        /// </summary>
        public virtual int Priority { get; } = 9999;
        
        /// <summary>
        /// Provides a method to define the container registrations
        /// </summary>
        /// <param name="builder"></param>
        protected abstract void Register(RegistrationBuilder builder);
        
        internal void Register(ContainerBuilder builder)
        {
            var b = new RegistrationBuilder();
            this.Register(b);

            b.Apply(builder);
        }

        public class ConfigurationBuilder
        {
            private readonly ICollection<Action<ContainerBuilder>> actions = new List<Action<ContainerBuilder>>();

            // TODO: abstract Autofac dependencies
            public void Include(Action<ContainerBuilder> registerAction)
            {
                this.actions.Add(registerAction);
            }

            internal void Apply(ContainerBuilder container)
            {
                foreach (var action in this.actions)
                {
                    action(container);
                }
            }
        }

        public class RegistrationBuilder : ConfigurationBuilder
        {
            public void Use<T>() where T : Module, new()
            {
                this.Include(b => b.RegisterModule<T>());
            }

            public void Use(Module module)
            {
                this.Include(b => b.RegisterModule(module));
            }
        }
    }
}