using System.Linq;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess.EntityFramework.Specification
{
    public abstract class ResultableKeyedQueryableSpecification<TSpec, TEntity, TPrimaryKey> :
        KeyedQueryableSpecification<TSpec, TEntity, TPrimaryKey>,
        IResultableSpecification<TEntity>
        where TSpec : class, IKeyedSpecification<TSpec, TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>, new()
    {
        protected ResultableKeyedQueryableSpecification(IQueryable<TEntity> queryable)
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