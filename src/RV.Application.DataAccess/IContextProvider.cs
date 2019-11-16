using System;

namespace RV.Application.DataAccess
{
    public interface IContextProvider<out T> : IDisposable where T: IContext
    {
        T Get();
    }
}