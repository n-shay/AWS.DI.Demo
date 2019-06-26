namespace HelloWorld.DynamoDB.Lambda.Models
{
    public class Input
    {
        public int? Id { get; set; }

        public int? Num { get; set; }

        public string TextSearch { get; set; }
    }
}