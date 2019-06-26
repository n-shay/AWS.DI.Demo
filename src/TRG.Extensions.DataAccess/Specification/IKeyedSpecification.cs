namespace TRG.Extensions.DataAccess.Specification
{
    public interface IKeyedSpecification<out TSpecification, TEntity, in TPrimaryKey> : ISpecification<TEntity>
        where TSpecification: IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
        where TEntity : IKeyedEntity<TPrimaryKey>
    {
        TSpecification ById(TPrimaryKey id);
    }
}