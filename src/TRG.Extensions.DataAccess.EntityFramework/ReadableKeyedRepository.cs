using System.Threading.Tasks;

namespace TRG.Extensions.DataAccess.EntityFramework
{
    using TRG.Extensions.DataAccess.Specification;
    using TRG.Extensions.DependencyInjection;
    using TRG.Extensions.Logging;

    public abstract class ReadableKeyedRepository<TEntity, TPrimaryKey, TSpecification>
        : ReadableRepository<TEntity, TSpecification>,
          IReadableKeyedRepository<TEntity, TPrimaryKey, TSpecification>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
        where TSpecification : ISpecification<TEntity>
    {
        protected ReadableKeyedRepository(ILogger logger, IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
            : base(logger, contextProvider, serviceProvider)
        {
        }

        public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
        {
            return await this.Context.Set<TEntity>().FindAsync(key);
        }
    }
}