namespace RV.Application.DataAccess
{
    public interface IFluentBulkUpdateDefinitionBuilder<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Define a bulk operation for the entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IFluentBulkOperationDefinition<TEntity> With(TEntity entity);

        /// <summary>
        /// Define a bulk operation for the entity on a specific field.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IFluentBulkFieldOperationDefinition<TEntity> For(TEntity entity);
    }
}