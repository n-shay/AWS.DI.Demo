namespace TRG.Extensions.DependencyInjection
{
    using System;

    public interface IServiceProvider
    {
        T Resolve<T>(params object[] arguments);

        object Resolve(Type serviceType, params object[] arguments);
    }
}