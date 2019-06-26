namespace TRG.Extensions.DependencyInjection
{
    internal class SelfDependencyDescriptor : DependencyDescriptor
    {
        private readonly IServiceProvider _serviceProvider;

        public SelfDependencyDescriptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(c => c.RegisterSingleton(() => _serviceProvider));
        }
    }
}