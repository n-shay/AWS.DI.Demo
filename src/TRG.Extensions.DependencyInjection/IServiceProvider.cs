namespace TRG.Extensions.DependencyInjection
{
    public interface IServiceProvider
    {
        T Resolve<T>()
            where T : class;
    }
}