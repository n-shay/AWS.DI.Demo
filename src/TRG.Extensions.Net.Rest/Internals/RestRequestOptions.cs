namespace TRG.Extensions.Net.Rest.Internals
{
    using System.Collections.Generic;

    internal class RestRequestOptions
    {
        public string BaseUrl { get; set; }

        public string DefaultContentType { get; set; }

        public ISerializer Serializer { get; set; }

        public ICollection<NameValuePair> DefaultHeaders { get; set; }

        public IRestClient Client { get; set; }
    }
}