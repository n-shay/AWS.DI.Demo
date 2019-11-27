namespace TRG.Extensions.DataAccess.EntityFramework.Specification
{
    using System.Linq;

    using TRG.Extensions.DataAccess.Specification;
    
    public class DerivedSpecificationResult<TEntity> : IDerivedSpecificationResult<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IQueryable<TEntity> queryable;

        public DerivedSpecificationResult(IQueryable<TEntity> queryable)
        {
            this.queryable = queryable;
        }

        public ISpecificationResult<TEntity> Result => new QueryableSpecificationResult<TEntity>(this.queryable);
    }
}
