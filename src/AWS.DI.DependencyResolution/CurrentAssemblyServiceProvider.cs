﻿using System;
using System.Linq;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace AWS.DI.DependencyResolution
{
    public class CurrentAssemblyServiceProvider : ServiceProviderBase
    {
        protected override void RegisterInternal(Container container)
        {
            var packages = this.GetType().Assembly.GetTypes()
                .Where(t => typeof(IPackage).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(t => (IPackage)Activator.CreateInstance(t))
                .ToList();

            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }

            // above code is faster
            //container.RegisterPackages(new[] { this.GetType().Assembly });
        }
    }
}