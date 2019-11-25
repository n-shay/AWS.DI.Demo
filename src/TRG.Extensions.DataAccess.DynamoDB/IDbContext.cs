namespace TRG.Extensions.DataAccess.DynamoDB
{
    using Amazon.DynamoDBv2.DataModel;

    public interface IDbContext : IDynamoDBContext, IContext
    {
    }
}