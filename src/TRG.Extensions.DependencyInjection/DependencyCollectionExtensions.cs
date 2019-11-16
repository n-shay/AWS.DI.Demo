namespace TRG.Extensions.DependencyInjection
{
    using Autofac;

    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseCurrentDomain(this IDependencyCollection collection)
        {
            collection.Add(new CurrentDomainDependencyDescriptor());
            return collection;
        }

        public static IDependencyCollection UseModule<T>(this IDependencyCollection collection) where T : Module, new()
        {
            collection.Add(new DependencyDescriptor<T>());
            return collection;
        }
    }
}