namespace TRG.Extensions.DataAccess
{
    public interface IContextFactory
    {
        IContext Create();
    }
}