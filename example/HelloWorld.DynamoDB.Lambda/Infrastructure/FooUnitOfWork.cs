using HelloWorld.DynamoDB.Lambda.Domain;
using HelloWorld.DynamoDB.Lambda.Domain.Repositories;
using TRG.Extensions.DataAccess.DynamoDB;
using TRG.Extensions.DependencyInjection;

namespace HelloWorld.DynamoDB.Lambda.Infrastructure
{
    public class FooUnitOfWork : UnitOfWork, IFooUnitOfWork
    {
        public IFooRepository Foos => GetRepository<IFooRepository>();

        public FooUnitOfWork(IDbContext context, IServiceProvider serviceProvider) 
            : base(context, serviceProvider)
        {
        }
    }
}