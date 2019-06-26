using Amazon.DynamoDBv2;
using SimpleInjector;
using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public static class DependencyCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static IDependencyCollection UseDynamoDB(this IDependencyCollection collection)
        {
            collection.Add(new DependencyDescriptor());
            return collection;
        }

        public class DependencyDescriptor : DependencyInjection.DependencyDescriptor
        {
            protected override void Register(RegistrationBuilder builder)
            {
                builder.Include(container =>
                {
                    container.RegisterSingleton<IAmazonDynamoDB>(() => new AmazonDynamoDBClient());
                    container.Register<IDbContext, Context>(GetContextLifestyle());
                });
            }

            protected virtual Lifestyle GetContextLifestyle()
            {
                return Lifestyle.Transient;
            }

        }
    }
}