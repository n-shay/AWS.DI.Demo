using Autofac;
using TRG.Extensions.DependencyInjection;
using TRG.Extensions.Lambda;

namespace TRG.Extensions.DataAccess.DynamoDB.Lambda
{
    public static class DependencyCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static IDependencyCollection UseDynamoDB(this IDependencyCollection collection)
        {
            collection.Add(new DependencyDescriptor());
            return collection;
        }

        public class DependencyDescriptor : DynamoDB.DependencyDescriptor
        {
        }
    }
}