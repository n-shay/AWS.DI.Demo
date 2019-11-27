using System;
using System.Linq;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess.EntityFramework.Specification
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for <see cref="T:System.Linq.IQueryable" /> based specifications.
    /// </summary>
    public abstract class QueryableSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="queryable"><see cref="IQueryable"/> for the specification to work with.</param>
        protected QueryableSpecification(IQueryable<TEntity> queryable)
        {
            this.Queryable = queryable ?? throw new ArgumentNullException(nameof(queryable));
            this.Queryable = this.Include();
        }

        protected virtual IQueryable<TEntity> Include()
        {
            return this.Queryable;
        }

        /// <summary>
        /// Gets or sets the queryable instance.
        /// </summary>
        protected IQueryable<TEntity> Queryable { get; set; }

        protected TSpec ToSpecification<TSpec, TSpecEntity>()
            where TSpec : QueryableSpecification<TSpecEntity>
            where TSpecEntity : class, TEntity
        {
            var spec = (TSpec) Activator.CreateInstance(typeof(TSpec), this.Queryable.OfType<TSpecEntity>());

            return spec;
        }
    }
}