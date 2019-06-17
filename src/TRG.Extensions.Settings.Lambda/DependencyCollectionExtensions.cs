using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.Settings.Lambda
{
    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection SetConfigurationProvider(this IDependencyCollection collection,
            IConfigurationProvider configurationProvider)
        {
            collection.Add(new DependencyDescriptor(configurationProvider));
            return collection;
        }

        public class DependencyDescriptor : DependencyInjection.DependencyDescriptor
        {
            public DependencyDescriptor(IConfigurationProvider configurationProvider)
            {
                IncludeRegister(c => c.RegisterInstance<IConfigurationProvider>(configurationProvider));
            }
        }
    }
}