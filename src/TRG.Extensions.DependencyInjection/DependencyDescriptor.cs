using System;
using System.Collections.Generic;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace TRG.Extensions.DependencyInjection
{
    public class DependencyDescriptor
    {
        private readonly ICollection<IPackage> _packages = new List<IPackage>();
        private readonly ICollection<Action<Container>> _registerActions = new List<Action<Container>>();

        protected void IncludePackage<T>() where T : IPackage, new()
        {
            IncludePackage(Activator.CreateInstance<T>());
        }

        protected void IncludePackage(IPackage package)
        {
            _packages.Add(package);
        }

        protected void IncludeRegister(Action<Container> registerAction)
        {
            _registerActions.Add(registerAction);
        }

        internal void Register(Container container)
        {
            foreach (var pkg in _packages)
            {
                pkg.RegisterServices(container);
            }

            foreach (var action in _registerActions)
            {
                action(container);
            }
        }
    }
}