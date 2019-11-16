using System.Threading.Tasks;
using RV.Application.Logging;

namespace RV.Application.DataAccess.DynamoDB
{
    public abstract class ReadableKeyedRepository<TEntity, TPrimaryKey>
        : ReadableRepository<TEntity>,
          IReadableKeyedRepository<TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
    {
        protected ReadableKeyedRepository(ILogger logger, IContextProvider<IDbContext> contextProvider)
            : base(logger, contextProvider)
        {
        }

        public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
        {
            return await Context.LoadAsync<TEntity>(key);
        }
    }
}