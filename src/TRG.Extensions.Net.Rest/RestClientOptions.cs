namespace TRG.Extensions.Net.Rest
{
    using System.Collections.Generic;

    public class RestClientOptions
    {
        public string BaseUrl { get; set; }

        public string DefaultContentType { get; set; }

        public ISerializer Serializer { get; set; }

        public IAuthenticator Authenticator { get; set; }

        public ICollection<NameValuePair> DefaultHeaders { get; }

        public RestClientOptions()
        {
            this.DefaultHeaders = new List<NameValuePair>();
        }
    }
}