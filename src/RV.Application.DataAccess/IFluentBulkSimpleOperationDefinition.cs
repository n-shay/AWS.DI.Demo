namespace RV.Application.DataAccess
{
    public interface IFluentBulkSimpleOperationDefinition<TEntity, in TField>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Increases the field by the <see cref="value"/> provided. If doesn't exists, field is set to <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        void Increment(TField value);

        /// <summary>
        /// Multiplies the field by the <see cref="value"/> provided. If doesn't exists, field is set to zero.
        /// </summary>
        /// <param name="value"></param>
        void Multiply(TField value);

        /// <summary>
        /// Updates the value of the field to a specified <see cref="value"/> if the specified value is less than the current value of the field.
        /// If the field does not exists, sets the field to the specified value.
        /// <see href="https://docs.mongodb.com/manual/reference/operator/update/min/"/> for more info.
        /// </summary>
        /// <param name="value"></param>
        void Min(TField value);

        /// <summary>
        /// Updates the value of the field to a specified <see cref="value"/> if the specified value is greater than the current value of the field.
        /// If the field does not exists, sets the field to the specified value.
        /// <see href="https://docs.mongodb.com/manual/reference/operator/update/max/"/> for more info.
        /// </summary>
        /// <param name="value"></param>
        void Max(TField value);
    }
}