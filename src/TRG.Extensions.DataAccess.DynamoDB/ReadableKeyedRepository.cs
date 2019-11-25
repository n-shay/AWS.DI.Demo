namespace TRG.Extensions.DataAccess.DynamoDB
{
    using System.Threading.Tasks;

    using TRG.Extensions.Logging;

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
            return await this.Context.LoadAsync<TEntity>(key);
        }
    }
}