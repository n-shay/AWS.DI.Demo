namespace TRG.Extensions.Net.Rest
{
    using System;

    using TRG.Extensions.Net.Rest.Internals;

    public class RestClient : IRestClient
    {
        private readonly RestClientOptions options;

        public RestClient(string baseUrl)
            : this(new RestClientOptions { BaseUrl = baseUrl })
        {
            if (baseUrl == null) throw new ArgumentNullException(nameof(baseUrl));
        }

        public RestClient(RestClientOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IRestClient SetDefaultContentType(string contentType)
        {
            this.options.DefaultContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            return this;
        }

        public IRestClient AddDefaultHeader(string name, string value)
        {
            this.options.DefaultHeaders.Add(new NameValuePair(name, value));
            return this;
        }

        public IRestClient SetSerializer(ISerializer serializer)
        {
            this.options.Serializer = serializer;
            return this;
        }

        public IRestClient SetAuthenticator(IAuthenticator authenticator)
        {
            this.options.Authenticator = authenticator;
            return this;
        }

        public IRestRequestBuilder CreateRequest()
        {
            if (this.options.BaseUrl == null) throw new RestException($"{nameof(this.options.BaseUrl)} is required.");

            return new RestRequestBuilder(this.options);
        }
    }
}