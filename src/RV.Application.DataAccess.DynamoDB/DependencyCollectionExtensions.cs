// ReSharper disable once CheckNamespace
namespace RV.Application.DependencyInjection
{
    public static class DependencyCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static IDependencyCollection UseDynamoDB(this IDependencyCollection collection)
        {
            collection.Add(new DataAccess.DynamoDB.DependencyDescriptor());
            return collection;
        }
    }
}