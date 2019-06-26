using System.Collections.Generic;
using HelloWorld.DynamoDB.Lambda.Domain.Models;

namespace HelloWorld.DynamoDB.Lambda.Models
{
    public class MultiItemOutput
    {
        public MultiItemOutput(IEnumerable<SomeFoo> value)
        {
            Value = value;
        }

        public IEnumerable<SomeFoo> Value { get; }
    }
}