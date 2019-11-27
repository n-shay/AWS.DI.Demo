using System.Linq;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess.EntityFramework.Specification
{
    public abstract class ResultableQueryableSpecification<TEntity> : QueryableSpecification<TEntity>,
        IResultableSpecification<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected ResultableQueryableSpecification(IQueryable<TEntity> queryable)
            : base(queryable)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns queryable specification result.
        /// </summary>
        public ISpecificationResult<TEntity> Result => new QueryableSpecificationResult<TEntity>(this.Queryable);
    }
}