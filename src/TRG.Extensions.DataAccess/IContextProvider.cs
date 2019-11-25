namespace TRG.Extensions.DataAccess
{
    using System;

    public interface IContextProvider<out T> : IDisposable where T: IContext
    {
        T Get();
    }
}