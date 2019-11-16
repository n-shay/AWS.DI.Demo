namespace RV.Application.DataAccess
{
    public interface IUnitOfWorkFactory
    {
        TUoW Create<TUoW>() where TUoW : IUnitOfWork;
    }
}