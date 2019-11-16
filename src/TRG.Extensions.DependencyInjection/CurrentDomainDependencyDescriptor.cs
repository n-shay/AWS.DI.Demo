namespace TRG.Extensions.DependencyInjection
{
    using System;

    using Autofac;

    internal class CurrentDomainDependencyDescriptor : DependencyDescriptor 
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container => container.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}