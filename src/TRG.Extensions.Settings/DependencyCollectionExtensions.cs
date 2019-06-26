using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Settings.Concrete;

namespace TRG.Extensions.Settings
{
    public static class DependencyCollectionExtensions
    {
        public static IDependencyCollection UseSettingProvider(this IDependencyCollection collection)
        {
            collection.Add(new DependencyDescriptor());
            return collection;
        }

        internal class DependencyDescriptor : DependencyInjection.DependencyDescriptor
        {
            protected override void Register(RegistrationBuilder builder)
            {
                builder.Include(c => { c.Register<ISettingsProvider, SettingsProvider>(); });
            }
        }
    }
}