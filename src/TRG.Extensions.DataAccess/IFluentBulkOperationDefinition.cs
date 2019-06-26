using System;
using System.Linq.Expressions;

namespace TRG.Extensions.DataAccess
{
    public interface IFluentBulkOperationDefinition<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Converts the definition to a partial update and defines the field for update (inclusive).
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldSelector"></param>
        /// <returns></returns>
        IFluentBulkPartialUpdateDefinition<TEntity> Include<TField>(Expression<Func<TEntity, TField>> fieldSelector);

        /// <summary>
        /// Adds an update operation to the bulk operation queue.
        /// </summary>
        void Update();

        /// <summary>
        /// Adds an upsert operation to the bulk operation queue.
        /// </summary>
        void Upsert();

        /// <summary>
        /// Adds an insert operation to the bulk operation queue.
        /// </summary>
        void Insert();

        /// <summary>
        /// Adds a delete operation to the bulk operation queue.
        /// </summary>
        void Delete();
    }
}