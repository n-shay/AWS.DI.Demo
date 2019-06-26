using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess
{
    public interface IReadableRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
    }

    public interface IReadableRepository<TEntity, out TSpecification> : IReadableRepository<TEntity>
        where TEntity : class, IEntity
        where TSpecification : ISpecification<TEntity>
    {
        /// <summary>
        /// Gets specification interface for complex searching for an entity or entities.
        /// </summary>
        /// <param name="includeArchive">If true and <see cref="TEntity"/> is <see cref="IDeletableEntity"/>, returns archived entities with results.</param>
        TSpecification Specify(bool includeArchive = false);
    }
}
