﻿namespace TRG.Extensions.Logging.Serilog
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Autofac;
    using Autofac.Core;

    using TRG.Extensions.DependencyInjection;

    internal class SerilogDependencyDescriptor : DependencyDescriptor
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Use<LoggingModule>();

            builder.Include(container =>
                container.RegisterType<SerilogLogger<object>>()
                    .As<ILogger>());
        }

        public class LoggingModule : Autofac.Module
        {
            private static void InjectLoggerProperties(object instance)
            {
                var instanceType = instance.GetType();

                // Get all the injectable properties to set.
                // If you wanted to ensure the properties were only UNSET properties,
                // here's where you'd do it.
                var properties = instanceType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite && p.GetIndexParameters().Length == 0);

                // Set the properties located.
                foreach (var propToSet in properties)
                {
                    propToSet.SetValue(instance, CreateLogger(instanceType), null);
                }
            }

            private static void OnComponentPreparing(object sender, PreparingEventArgs e)
            {
                e.Parameters = e.Parameters.Union(
                    new[]
                    {
                        new ResolvedParameter(
                            (p, i) => p.ParameterType == typeof(ILogger),
                            (p, i) => CreateLogger(p.Member.DeclaringType)
                        ),
                    });
            }

            protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
            {
                // Handle constructor parameters.
                registration.Preparing += OnComponentPreparing;

                // Handle properties.
                registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
            }

            private static object CreateLogger(Type instanceType)
            {
                return Activator.CreateInstance(typeof(SerilogLogger<>).MakeGenericType(instanceType));
            }
        }
    }
}