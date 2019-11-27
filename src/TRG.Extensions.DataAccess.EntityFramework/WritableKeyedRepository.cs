using System;
using System.Threading.Tasks;
using TRG.Extensions.DataAccess.Specification;
using TRG.Extensions.Logging;
using IServiceProvider = TRG.Extensions.DependencyInjection.IServiceProvider;

namespace TRG.Extensions.DataAccess.EntityFramework
{
    public abstract class WritableKeyedRepository<TEntity, TPrimaryKey, TSpec>
        : WritableRepository<TEntity, TSpec>,
          IWritableKeyedRepository<TEntity, TPrimaryKey, TSpec>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
        where TSpec : IKeyedSpecification<TSpec, TEntity, TPrimaryKey>
    {
        protected WritableKeyedRepository(ILogger logger, IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
            : base(logger, contextProvider, serviceProvider)
        {
        }

        public IFluentBulkUpdateDefinitionBuilder<TEntity> BulkOperation => throw new NotImplementedException();

        public Task<int> ExecuteBulk(bool isOrdered = true)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
        {
            return await this.Context.Set<TEntity>().FindAsync(key);
        }
    }
}