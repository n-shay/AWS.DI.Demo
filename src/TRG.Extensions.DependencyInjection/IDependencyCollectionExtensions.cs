using SimpleInjector.Packaging;

namespace TRG.Extensions.DependencyInjection
{
    public static class IDependencyCollectionExtensions
    {
        public static IDependencyCollection UseCurrentDomain(this IDependencyCollection collection)
        {
            collection.Add(new CurrentDomainDependencyDescriptor());
            return collection;
        }

        public static IDependencyCollection UsePackage<T>(this IDependencyCollection collection) where T : IPackage, new()
        {
            collection.Add(new DependencyDescriptor<T>());
            return collection;
        }
    }
}