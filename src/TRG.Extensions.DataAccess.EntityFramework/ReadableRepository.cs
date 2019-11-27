namespace TRG.Extensions.DataAccess.EntityFramework
{
    using System.Linq;

    using TRG.Extensions.DataAccess.Specification;
    using TRG.Extensions.DependencyInjection;
    using TRG.Extensions.Logging;

    public abstract class ReadableRepository<TEntity, TSpecification> :
        RepositoryBase,
        IReadableRepository<TEntity, TSpecification>
        where TEntity : class, IEntity
        where TSpecification : ISpecification<TEntity>
    {
        protected ReadableRepository(ILogger logger, IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
            : base(logger, contextProvider, serviceProvider)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets specification interface for complex searching for an entity or entities.
        /// </summary>
        public TSpecification Specify(bool includeArchive = false)
        {
            var dbSet = this.Context.Set<TEntity>();

            var queryable = dbSet.AsQueryable();

            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(TEntity)) && !includeArchive)
            {
                queryable = queryable.ExcludeDeleted();
            }

            return this.ServiceProvider.Resolve<TSpecification>(queryable);
        }
    }
}