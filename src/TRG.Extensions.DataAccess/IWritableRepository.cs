using System.Threading.Tasks;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess
{
    /// <summary>
    /// Generic repository interface (DDD) for reading and writing domain entities to a storage.
    /// </summary>
    /// <typeparam name="TEntity">Domain entity.</typeparam>
    public interface IWritableRepository<TEntity> : IReadableRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Creates a new instance of <see cref="TEntity"/>.
        /// </summary>
        /// <returns></returns>
        T New<T>()
            where T : TEntity, new();

        /// <summary>
        /// Inserts entity to the storage.
        /// </summary>
        Task Add<T>(T entity)
            where T : TEntity;

        /// <summary>
        /// Attaches entity retrieved by external unit of work (or created manually) to the storage.
        /// </summary>
        Task Attach<T>(ref T entity)
            where T : TEntity;

        /// <summary>
        /// Updates entity in the storage.
        /// </summary>
        Task Update<T>(T entity)
            where T : TEntity;

        /// <summary>
        /// Deletes entity in the storage.
        /// </summary>
        Task Delete<T>(T entity)
            where T : TEntity;
    }

    /// <summary>
    /// Generic repository interface (DDD) for reading and writing domain entities to a storage.
    /// </summary>
    /// <typeparam name="TEntity">Domain entity.</typeparam>
    /// <typeparam name="TSpecification"></typeparam>
    public interface IWritableRepository<TEntity, out TSpecification> 
        : IWritableRepository<TEntity>, IReadableRepository<TEntity, TSpecification>
        where TEntity : class, IEntity
        where TSpecification : ISpecification<TEntity>
    {

    }
}
