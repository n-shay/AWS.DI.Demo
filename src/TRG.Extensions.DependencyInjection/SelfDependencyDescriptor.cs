namespace TRG.Extensions.DependencyInjection
{
    using Autofac;

    internal class SelfDependencyDescriptor : DependencyDescriptor
    {
        private readonly IServiceProvider serviceProvider;

        public SelfDependencyDescriptor(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(c => c.RegisterInstance(this.serviceProvider).As<IServiceProvider>());
        }
    }
}