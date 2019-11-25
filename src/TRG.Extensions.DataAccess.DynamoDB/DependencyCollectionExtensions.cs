namespace TRG.Extensions.DataAccess.DynamoDB
{
    using TRG.Extensions.DependencyInjection;

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