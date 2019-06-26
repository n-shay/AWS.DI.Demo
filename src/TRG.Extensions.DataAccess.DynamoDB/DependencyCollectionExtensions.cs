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
    }
}