namespace TRG.Extensions.DataAccess.DynamoDB
{
    using System;

    public class DynamoDbException : ApplicationException
    {
        public DynamoDbException(string message)
            : base(message)
        {
        }

        public DynamoDbException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}