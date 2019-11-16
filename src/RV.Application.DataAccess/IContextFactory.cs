namespace RV.Application.DataAccess
{
    public interface IContextFactory<out T> where T: IContext
    {
        T Create();
    }
}