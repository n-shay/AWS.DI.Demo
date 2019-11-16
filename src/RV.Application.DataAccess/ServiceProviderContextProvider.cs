using RV.Application.DependencyInjection;

namespace RV.Application.DataAccess
{
    public class ServiceProviderContextProvider<TContext> : IContextProvider<TContext>
        where TContext : IContext
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderContextProvider(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public TContext Get() 
        {
            return _serviceProvider.Resolve<TContext>();
        }

        public void Dispose()
        {
        }
    }
}