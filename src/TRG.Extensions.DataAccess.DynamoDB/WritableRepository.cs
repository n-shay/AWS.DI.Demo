using System;
using System.Threading.Tasks;
using TRG.Extensions.Logging;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class WritableRepository<TEntity> :
        ReadableRepository<TEntity>,
        IWritableRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected WritableRepository(ILogger logger, IDbContext context)
            : base(logger, context)
        {
        }

        public T New<T>()
            where T : TEntity, new()
        {
            return new T();
        }

        public async Task Add<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Context.SaveAsync(entity);
        }

        public Task Attach<T>(ref T entity)
            where T : TEntity
        {
            return Task.CompletedTask;
        }

        public async Task Update<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Context.SaveAsync(entity);
        }

        public async Task Delete<T>(T entity)
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
                await Context.SaveAsync(entity);
            }
            else
            {
                await Context.DeleteAsync(entity);
            }
        }
    }
}