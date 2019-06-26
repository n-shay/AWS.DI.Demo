using HelloWorld.DynamoDB.Lambda.Business;
using HelloWorld.DynamoDB.Lambda.Business.Concrete;
using HelloWorld.DynamoDB.Lambda.Domain;
using HelloWorld.DynamoDB.Lambda.Domain.Repositories;
using HelloWorld.DynamoDB.Lambda.Infrastructure;
using HelloWorld.DynamoDB.Lambda.Infrastructure.DynamoDB;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace HelloWorld.DynamoDB.Lambda.DependencyResolution
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            // Business
            container.Register<IFooService, FooService>();

            // Infrastructure
            container.Register<IFooUnitOfWork, FooUnitOfWork>();
            container.Register<IFooRepository, FooRepository>();
        }
    }
}