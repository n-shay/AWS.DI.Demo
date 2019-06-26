namespace TRG.Extensions.DataAccess
{
    public interface IKeyedEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; set; }
    }
}
