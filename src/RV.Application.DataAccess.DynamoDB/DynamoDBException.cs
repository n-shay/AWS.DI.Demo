using System;

namespace RV.Application.DataAccess.DynamoDB
{
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