namespace TRG.Extensions.DataAccess.Specification
{
    /// <summary>
    /// Generic base interface for including multiple derived class entities in a single query.
    /// </summary>
    public interface IDerivedSpecificationResult<TEntity> 
        where TEntity: IEntity
    {
        /// <summary>
        /// Gets specification result wrapped into <see cref="ISpecificationResult{TEntity}"/> interface.
        /// </summary>
        /// <remarks>
        /// <see cref="ISpecificationResult{TEntity}"/> interface wraps common functionality that is shared
        /// across all specifications.
        /// </remarks>
        ISpecificationResult<TEntity> Result { get; }
    }
}