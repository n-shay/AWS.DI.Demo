using RV.Application.DependencyInjection;

namespace RV.Application.DataAccess.DynamoDB
{
    public abstract class UnitOfWork : UnitOfWork<IDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        protected UnitOfWork(IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
            : base(contextProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override TRepository InstantiateRepository<TRepository>()
        {
            var rep =
                _serviceProvider.Resolve<TRepository>(ContextProvider);

            if (rep == null)
                throw new DependencyResolutionException(typeof(TRepository));

            return rep;
        }
    }
}