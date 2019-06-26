using System.Threading.Tasks;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess
{
    public interface IWritableKeyedRepository<TEntity, TPrimaryKey>
        : IReadableKeyedRepository<TEntity, TPrimaryKey>, IWritableRepository<TEntity>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
    {
        IFluentBulkUpdateDefinitionBuilder<TEntity> BulkOperation { get; }

        /// <summary>
        /// Executes all bulk updates.
        /// </summary>
        /// <param name="isOrdered">True if the bulk operation should be executed in order. False to execute in parallel.</param>
        /// <returns>Number of modified records.</returns>
        Task<int> ExecuteBulk(bool isOrdered = true);
    }

    public interface IWritableKeyedRepository<TEntity, TPrimaryKey, out TSpecification>
        : IReadableKeyedRepository<TEntity, TPrimaryKey, TSpecification>, IWritableKeyedRepository<TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
        where TSpecification : IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
    {
    }
}
