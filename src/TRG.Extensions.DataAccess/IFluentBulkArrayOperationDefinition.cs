namespace TRG.Extensions.DataAccess
{
    public interface IFluentBulkArrayOperationDefinition<TEntity, in TField>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Adds a value to an array unless the value is already present, in which case it does nothing to that array.
        /// </summary>
        /// <param name="values"></param>
        void AddToSet(params TField[] values);

        /// <summary>
        /// Appends the specified <see cref="values"/> to an array.
        /// </summary>
        /// <param name="values"></param>
        void Push(params TField[] values);

        /// <summary>
        /// Removes from an existing array all instances of the specified <see cref="values"/>.
        /// </summary>
        /// <param name="values"></param>
        void Pull(params TField[] values);

        /// <summary>
        /// Removes the first element of an array.
        /// </summary>
        void PopFirst();

        /// <summary>
        /// Removes the last element of an array.
        /// </summary>
        void PopLast();
    }
}