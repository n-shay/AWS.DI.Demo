using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TRG.Extensions.DataAccess
{
    public interface IFluentBulkFieldOperationDefinition<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Specifies a single field to perform the operation on
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldSelector"></param>
        /// <returns></returns>
        IFluentBulkSimpleOperationDefinition<TEntity, TField> OnField<TField>(Expression<Func<TEntity, TField>> fieldSelector);

        IFluentBulkArrayOperationDefinition<TEntity, TField> OnArray<TField>(Expression<Func<TEntity, IEnumerable<TField>>> fieldSelector);
    }
}