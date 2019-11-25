namespace TRG.Extensions.DataAccess.DynamoDB
{
    using TRG.Extensions.DependencyInjection;

    public abstract class UnitOfWork : UnitOfWork<IDbContext>
    {
        private readonly IServiceProvider serviceProvider;

        protected UnitOfWork(IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
            : base(contextProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override TRepository InstantiateRepository<TRepository>()
        {
            var rep =
                this.serviceProvider.Resolve<TRepository>(this.ContextProvider);

            if (rep == null)
                throw new DependencyResolutionException(typeof(TRepository));

            return rep;
        }
    }
}