using Amazon.DynamoDBv2;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public class Context : ContextBase
    {
        public Context(IAmazonDynamoDB client) : base(client)
        {
        }
    }
}