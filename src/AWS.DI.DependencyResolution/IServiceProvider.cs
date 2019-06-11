namespace AWS.DI.DependencyResolution
{
    public interface IServiceProvider
    {
        T Resolve<T>()
            where T : class;
    }
}