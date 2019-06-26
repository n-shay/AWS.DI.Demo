using Amazon.DynamoDBv2;
using SimpleInjector;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public class DependencyDescriptor : DependencyInjection.DependencyDescriptor
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container =>
            {
                container.RegisterSingleton<IAmazonDynamoDB>(() => new AmazonDynamoDBClient());
                container.Register<IDbContext, Context>(GetContextLifestyle());
                container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            });
        }

        protected virtual Lifestyle GetContextLifestyle()
        {
            return Lifestyle.Transient;
        }

    }
}