using System;

namespace TRG.Extensions.DependencyInjection
{
    public interface IServiceProvider
    {
        T Resolve<T>();

        object Resolve(Type serviceType);
    }
}