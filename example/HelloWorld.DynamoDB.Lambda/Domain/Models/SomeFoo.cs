using Amazon.DynamoDBv2.DataModel;
using TRG.Extensions.DataAccess;

namespace HelloWorld.DynamoDB.Lambda.Domain.Models
{
    [DynamoDBTable("FooTable")]
    public class SomeFoo: IKeyedEntity<int>
    {
        [DynamoDBHashKey("SomeId")]
        public int Id { get; set; }

        [DynamoDBProperty("SomeInt")]
        public int Num { get; set; }

        [DynamoDBProperty("SomeString")]
        public string Text { get; set; }

        public override string ToString()
        {
            return $"({nameof(Id)}: {Id} | {nameof(Num)}: {Num} | {nameof(Text)}: '{Text}')";
        }
    }
}