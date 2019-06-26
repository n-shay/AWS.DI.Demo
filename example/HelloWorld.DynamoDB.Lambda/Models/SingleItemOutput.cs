using HelloWorld.DynamoDB.Lambda.Domain.Models;

namespace HelloWorld.DynamoDB.Lambda.Models
{
    public class SingleItemOutput
    {
        public SingleItemOutput(SomeFoo value)
        {
            Value = value;
        }

        public SomeFoo Value { get; }
    }
}