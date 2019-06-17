using SimpleInjector.Packaging;

namespace TRG.Extensions.DependencyInjection
{
    internal class DependencyDescriptor<T> : DependencyDescriptor where T: IPackage, new()
    {
        public DependencyDescriptor()
        {
            IncludePackage<T>();
        }
    }
}