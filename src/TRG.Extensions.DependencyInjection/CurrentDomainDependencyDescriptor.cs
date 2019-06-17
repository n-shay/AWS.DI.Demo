using System;
using SimpleInjector;

namespace TRG.Extensions.DependencyInjection
{
    internal class CurrentDomainDependencyDescriptor : DependencyDescriptor 
    {
        public CurrentDomainDependencyDescriptor()
        {
            IncludeRegister(container => container.RegisterPackages(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}