namespace TRG.Extensions.DataAccess
{
    public interface IContextFactory<out T> where T: IContext
    {
        T Create();
    }
}