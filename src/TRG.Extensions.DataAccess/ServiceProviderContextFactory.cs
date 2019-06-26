using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.DataAccess
{
    public class ServiceProviderContextFactory : IContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderContextFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IContext Create()
        {
            return this._serviceProvider.Resolve<IContext>();
        }
    }
}