using System;

namespace TRG.Extensions.DataAccess
{
    public interface IUnitOfWorkFactory
    {
        TUoW Create<TUoW>() where TUoW : IUnitOfWork;
    }
}