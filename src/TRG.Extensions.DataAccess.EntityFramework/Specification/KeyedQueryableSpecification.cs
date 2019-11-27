using System.Linq;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess.EntityFramework.Specification
{
    public abstract class KeyedQueryableSpecification<TSpec, TEntity, TPrimaryKey> : QueryableSpecification<TEntity>,
        IKeyedSpecification<TSpec, TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
        where TSpec : class, IKeyedSpecification<TSpec, TEntity, TPrimaryKey>
    {
        protected KeyedQueryableSpecification(IQueryable<TEntity> queryable)
            : base(queryable)
        {
        }

        public TSpec ById(TPrimaryKey id)
        {
            this.Queryable = this.Queryable.Where(e => e.Id.Equals(id));
            return this as TSpec;
        }
    }
}