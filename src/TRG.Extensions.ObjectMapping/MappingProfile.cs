namespace TRG.Extensions.ObjectMapping
{
    using AutoMapper;

    public abstract class MappingProfile
    {
        protected abstract void Configure(IMapperConfigurationExpression config);

        internal void ConfigureInternal(IMapperConfigurationExpression mapperConfiguration)
        {
            this.Configure(mapperConfiguration);
        }
    }
}