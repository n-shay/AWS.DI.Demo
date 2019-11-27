namespace TRG.Extensions.DataAccess.EntityFramework
{
    using System;
    using System.Threading.Tasks;
    using TRG.Extensions.DataAccess.Specification;
    using TRG.Extensions.Logging;

    public abstract class WritableRepository<TEntity, TSpecification> :
        ReadableRepository<TEntity, TSpecification>,
        IWritableRepository<TEntity, TSpecification>
        where TEntity : class, IEntity
        where TSpecification : ISpecification<TEntity>
    {
        protected WritableRepository(ILogger logger, IContextProvider<IDbContext> contextProvider, DependencyInjection.IServiceProvider serviceProvider)
            : base(logger, contextProvider, serviceProvider)
        {
        }

        public T New<T>()
            where T : TEntity, new()
        {
            return new T();
        }

        public Task AddAsync<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Context.Set<TEntity>().Add(entity);

            return Task.CompletedTask;
        }

        public Task AttachAsync<T>(ref T entity)
            where T : TEntity
        {
            if (this.Context.IsDetached(entity))
            {
                entity = (T) this.Context.Set<TEntity>().Attach(entity).Entity;
            }

            return Task.CompletedTask;
        }

        public Task UpdateAsync<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (this.Context.IsDetached(entity))
            {
                throw new EntityFrameworkException("Entity is not attached to the context.");
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync<T>(T entity)
            where T : TEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (this.Context.IsDetached(entity))
            {
                entity = (T)this.Context.Set<TEntity>().Attach(entity).Entity;
            }

            // If the IDeletableEntity interface is used, flag the item as deleted rather than removing.
            if (entity is IDeletableEntity deletableEntity)
            {
                deletableEntity.IsDeleted = true;
            }
            else
            {
                this.Context.Set<TEntity>().Remove(entity);
            }

            return Task.CompletedTask;
        }
    }
}