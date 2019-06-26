using HelloWorld.DynamoDB.Lambda.Domain.Repositories;
using TRG.Extensions.DataAccess;

namespace HelloWorld.DynamoDB.Lambda.Domain
{
    public interface IFooUnitOfWork: IUnitOfWork
    {
        IFooRepository Foos { get; }
    }
}