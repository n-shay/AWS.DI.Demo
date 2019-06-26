using System;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public class DynamoDbException : Exception
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