namespace TRG.Extensions.DataAccess.DynamoDB
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;

    public abstract class ContextBase : DynamoDBContext, IDbContext
    {
        protected ContextBase(IAmazonDynamoDB client)
            : base(client)
        {
        }
    }
}