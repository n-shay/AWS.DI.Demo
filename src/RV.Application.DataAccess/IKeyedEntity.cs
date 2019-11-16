namespace RV.Application.DataAccess
{
    public interface IKeyedEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; set; }
    }
}
