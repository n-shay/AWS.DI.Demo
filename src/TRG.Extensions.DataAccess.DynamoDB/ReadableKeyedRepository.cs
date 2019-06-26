using System.Threading.Tasks;
using TRG.Extensions.Logging;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class ReadableKeyedRepository<TEntity, TPrimaryKey>
        : ReadableRepository<TEntity>,
          IReadableKeyedRepository<TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
    {
        protected ReadableKeyedRepository(ILogger logger, IDbContext context)
            : base(logger, context)
        {
        }

        public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
        {
            return await Context.LoadAsync<TEntity>(key);
        }
    }
}