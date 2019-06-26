using Amazon.DynamoDBv2.DataModel;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public interface IDbContext : IDynamoDBContext, IContext
    {
    }
}