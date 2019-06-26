﻿using TRG.Extensions.DependencyInjection;

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

        internal class DependencyDescriptor : DependencyInjection.DependencyDescriptor
        {
            private readonly IConfigurationProvider _configurationProvider;

            public DependencyDescriptor(IConfigurationProvider configurationProvider)
            {
                _configurationProvider = configurationProvider;
            }

            protected override void Register(RegistrationBuilder builder)
            {
                builder.Include(c => c.RegisterInstance(_configurationProvider));
            }
        }
    }
}