namespace RV.Application.DataAccess.Specification
{
    public interface IAbstractResultableSpecification<TEntity> : ISpecification<TEntity> 
        where TEntity : IEntity
    {
        /// <summary>
        /// Specifies a derived entity type to include in query result.
        /// </summary>
        IDerivedSpecificationResult<TEntity> AsAny();
    }
}
