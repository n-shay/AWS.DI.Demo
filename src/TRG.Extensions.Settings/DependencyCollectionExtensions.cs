using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Settings.Concrete;

namespace TRG.Extensions.Settings
{
    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseSettingProvider(this IDependencyCollection collection)
        {
            collection.Add(new InternalDependencyDescriptor());
            return collection;
        }

        internal class InternalDependencyDescriptor : DependencyDescriptor
        {
            public InternalDependencyDescriptor()
            {
                IncludeRegister(c =>
                {
                    c.Register<ISettingsProvider, SettingsProvider>();
                });
            }
        }
    }
}