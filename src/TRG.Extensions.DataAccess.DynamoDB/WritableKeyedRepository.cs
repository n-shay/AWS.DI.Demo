using System;
using System.Threading.Tasks;
using TRG.Extensions.Logging;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class WritableKeyedRepository<TEntity, TPrimaryKey>
        : WritableRepository<TEntity>, 
          IWritableKeyedRepository<TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
    {
        protected WritableKeyedRepository(ILogger logger, IDbContext context)
            : base(logger, context)
        {
        }

        public IFluentBulkUpdateDefinitionBuilder<TEntity> BulkOperation => throw new NotImplementedException();

        public Task<int> ExecuteBulk(bool isOrdered = true)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
        {
            return await Context.LoadAsync<TEntity>(key);
        }
    }
}