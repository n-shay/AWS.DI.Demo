using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class UnitOfWork : UnitOfWork<IDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        protected UnitOfWork(IDbContext context, IServiceProvider serviceProvider)
            : base(context)
        {
            _serviceProvider = serviceProvider;
        }

        protected override TRepository InstantiateRepository<TRepository>()
        {
            var rep =
                _serviceProvider.Resolve<TRepository>();

            if (rep == null)
                throw new DependencyResolutionException(typeof(TRepository));
            
            return rep;
        }
    }
}