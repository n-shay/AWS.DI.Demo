namespace TRG.Extensions.DependencyInjection
{
    using Autofac;

    internal class DependencyDescriptor<T> : DependencyDescriptor where T: Module, new()
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Use<T>();
        }
    }
}