using HelloWorld.DynamoDB.Lambda.Domain;
using HelloWorld.DynamoDB.Lambda.Domain.Repositories;
using HelloWorld.DynamoDB.Lambda.Infrastructure;
using HelloWorld.DynamoDB.Lambda.Infrastructure.DynamoDB;
using SimpleInjector;
using SimpleInjector.Packaging;
using TRG.Extensions.DataAccess;

namespace HelloWorld.DynamoDB.Lambda.DependencyResolution
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            // Business
            //container.Register<ISpeakService, SpeakService>();

            // Infrastructure
            // UnitOfWork
            container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.Register<IFooUnitOfWork, FooUnitOfWork>();
            container.Register<IFooRepository, FooRepository>();
        }
    }
}