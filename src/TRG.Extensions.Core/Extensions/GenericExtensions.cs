namespace TRG.Extensions
{
    using System.Text.Json;

    public static class GenericExtensions
    {
        public static T DeepClone<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (source == null)
            {
                return default;
            }

            var serializerOptions = new JsonSerializerOptions { };
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source, serializerOptions), serializerOptions);
        }
    }
}