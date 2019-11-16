namespace TRG.Extensions.Net.Rest
{
    public class NameValuePair
    {
        public NameValuePair(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public bool IsEmpty => this.Name == null;

        public static NameValuePair Empty = new NameValuePair(null, null);
    }
}