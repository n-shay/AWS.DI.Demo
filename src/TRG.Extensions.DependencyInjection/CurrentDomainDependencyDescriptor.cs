using System;
using SimpleInjector;

namespace TRG.Extensions.DependencyInjection
{
    internal class CurrentDomainDependencyDescriptor : DependencyDescriptor 
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container => container.RegisterPackages(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}