using HelloWorld.DynamoDB.Lambda.Domain.Models;

namespace HelloWorld.DynamoDB.Lambda.Models
{
    public class Output
    {
        public Output(SomeFoo value)
        {
            Value = value;
        }

        public SomeFoo Value { get; }
    }
}