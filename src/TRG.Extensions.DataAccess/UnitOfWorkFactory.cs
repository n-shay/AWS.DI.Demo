using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TUoW Create<TUoW>() where TUoW : IUnitOfWork
        {
            return _serviceProvider.Resolve<TUoW>();
        }
    }
}