namespace TRG.Extensions.ObjectMapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    public class MapperOptions
    {
        private readonly ICollection<MappingProfile> profiles = new List<MappingProfile>();
        private bool enableConfigurationValidation;

        public MapperOptions EnableConfigurationValidation()
        {
            this.enableConfigurationValidation = true;
            return this;
        }

        public MapperOptions AddProfile<T>() where T: MappingProfile, new()
        {
            this.profiles.Add(Activator.CreateInstance<T>());
            return this;
        }

        internal IMapper CreateMapper()
        {
            if(!this.profiles.Any())
                throw new ConfigurationException("At least one mapping profile must be added.");

            try
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in this.profiles)
                    {
                        profile.ConfigureInternal(cfg);
                    }
                });

                if(this.enableConfigurationValidation)
                    configuration.AssertConfigurationIsValid();

                return configuration.CreateMapper();
            }
            catch (Exception ex)
            {
                throw new ConfigurationException("Failed to build mapper from provided profile(s).", ex);
            }
        }
    }
}