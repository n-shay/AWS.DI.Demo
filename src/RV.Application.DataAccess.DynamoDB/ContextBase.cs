using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace RV.Application.DataAccess.DynamoDB
{
    public abstract class ContextBase : DynamoDBContext, IDbContext
    {
        protected ContextBase(IAmazonDynamoDB client)
            : base(client)
        {
        }
    }
}