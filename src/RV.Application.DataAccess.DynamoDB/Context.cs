using Amazon.DynamoDBv2;

namespace RV.Application.DataAccess.DynamoDB
{
    public class Context : ContextBase
    {
        public Context(IAmazonDynamoDB client) : base(client)
        {
        }
    }
}