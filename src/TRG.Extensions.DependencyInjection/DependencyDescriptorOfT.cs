using SimpleInjector.Packaging;

namespace TRG.Extensions.DependencyInjection
{
    internal class DependencyDescriptor<T> : DependencyDescriptor where T: IPackage, new()
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Use<T>();
        }
    }
}