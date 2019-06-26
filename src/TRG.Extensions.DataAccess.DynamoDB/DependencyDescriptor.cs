using Amazon.DynamoDBv2;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class DependencyDescriptor : DependencyInjection.DependencyDescriptor
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container =>
            {
                container.RegisterSingleton<IAmazonDynamoDB>(() => new AmazonDynamoDBClient());
                container.Register<IDbContext, Context>(GetContextLifestyle());
            });
        }

        protected abstract Lifestyle GetContextLifestyle();
    }
}