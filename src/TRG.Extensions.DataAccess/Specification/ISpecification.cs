namespace TRG.Extensions.DataAccess.Specification
{
    /// <summary>
    /// Generic base interface for the domain specifications (domain oriented queries).
    /// </summary>
    /// <remarks>
    /// Specification pattern belongs to the domain patterns family. It is used in
    /// Domain Driven Design (DDD) world. Name of the methods of derivate interfaces
    /// are matching the domain and it's context. It is domain oriented.
    ///
    /// In comparison the Query pattern belongs to the (generic) design patterns family. 
    /// The pattern is focused more on generic and technical side of the problem.
    /// Example is NHibernate and its criterion query objects pattern implementation.
    /// </remarks>
    public interface ISpecification<TEntity> where TEntity: IEntity
    {
    }
}