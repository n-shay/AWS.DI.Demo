namespace TRG.Extensions.DataAccess
{
    using TRG.Extensions.DependencyInjection;

    public class ServiceProviderContextProvider<TContext> : IContextProvider<TContext>
        where TContext : IContext
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TContext Get() 
        {
            return this.serviceProvider.Resolve<TContext>();
        }

        public void Dispose()
        {
        }
    }
}