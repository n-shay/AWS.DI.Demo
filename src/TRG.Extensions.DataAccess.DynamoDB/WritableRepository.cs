namespace TRG.Extensions.DataAccess.DynamoDB
{
    using System;
    using System.Threading.Tasks;

    using TRG.Extensions.Logging;

    public abstract class WritableRepository<TEntity> :
        ReadableRepository<TEntity>,
        IWritableRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected WritableRepository(ILogger logger, IContextProvider<IDbContext> contextProvider)
            : base(logger, contextProvider)
        {
        }

        public T New<T>()
            where T : TEntity, new()
        {
            return new T();
        }

        public async Task AddAsync<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Context.SaveAsync(entity);
        }

        public Task AttachAsync<T>(ref T entity)
            where T : TEntity
        {
            return Task.CompletedTask;
        }

        public async Task UpdateAsync<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Context.SaveAsync(entity);
        }

        public async Task DeleteAsync<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // If the IDeletableEntity interface is used, flag the item as deleted rather than removing.
            if (entity is IDeletableEntity deletableEntity)
            {
                deletableEntity.IsDeleted = true;
                await this.Context.SaveAsync(entity);
            }
            else
            {
                await this.Context.DeleteAsync(entity);
            }
        }
    }
}