namespace TRG.Extensions.DataAccess.DynamoDB
{
    using Amazon.DynamoDBv2;

    public class Context : ContextBase
    {
        public Context(IAmazonDynamoDB client) : base(client)
        {
        }
    }
}