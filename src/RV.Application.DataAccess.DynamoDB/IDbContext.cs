using Amazon.DynamoDBv2.DataModel;

namespace RV.Application.DataAccess.DynamoDB
{
    public interface IDbContext : IDynamoDBContext, IContext
    {
    }
}