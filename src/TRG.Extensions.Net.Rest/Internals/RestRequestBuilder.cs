namespace TRG.Extensions.Net.Rest.Internals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    internal class RestRequestBuilder : IRestRequestBuilder
    {
        private readonly RestClientOptions clientOptions;
        private HttpVerb method = HttpVerb.Get;
        private string contentType;
        private string relativeUrl;
        private readonly ICollection<NameValuePair> headers = new List<NameValuePair>();
        private readonly ICollection<NameValuePair> parameters = new List<NameValuePair>();

        public RestRequestBuilder(RestClientOptions clientOptions)
        {
            this.clientOptions = clientOptions;
        }

        public IRestRequestBuilder AsMethod(HttpVerb method)
        {
            if (!Enum.IsDefined(typeof(HttpVerb), method))
                throw new InvalidEnumArgumentException(nameof(method), (int)method, typeof(HttpVerb));

            this.method = method;
            return this;
        }

        public IRestRequestBuilder AsPost()
        {
            this.method = HttpVerb.Post;
            return this;
        }

        public IRestRequestBuilder AsGet()
        {
            this.method = HttpVerb.Get;
            return this;
        }

        public IRestRequestBuilder AddHeader(string name, string value)
        {
            this.headers.Add(new NameValuePair(name, value));
            return this;
        }

        public IRestRequestBuilder AddParameter(string name, string value)
        {
            this.parameters.Add(new NameValuePair(name, value));
            return this;
        }

        public IRestRequestBuilder WithContentType(string contentType)
        {
            this.contentType = contentType;
            return this;
        }

        public IRestRequestBuilder WithUrl(string relativeUrl)
        {
            this.relativeUrl = relativeUrl;
            return this;
        }

        public IRestRequest Build()
        {
            var request = new RestRequest
            {
                Url = this.GetUrlInternal(),
                Method = this.method,
                ContentType = this.contentType ?? this.clientOptions.DefaultContentType,
                Serializer = this.clientOptions.Serializer,
                Authenticator = this.clientOptions.Authenticator
            };

            // combine headers
            var headers = this.headers.Where(nvp => !nvp.IsEmpty).ToList();
            foreach (var defaultHeader in this.clientOptions.DefaultHeaders.Where(nvp => !nvp.IsEmpty))
            {
                if (headers.All(h => h.Name != defaultHeader.Name))
                    headers.Add(defaultHeader);
            }

            headers.ForEach(request.Headers.Add);

            // parameters
            this.parameters.Where(p => !p.IsEmpty).ToList()
                .ForEach(request.Parameters.Add);

            return request;
        }

        private string GetUrlInternal()
        {
            return new[] { this.clientOptions.BaseUrl.TrimEnd('/'), this.relativeUrl?.TrimStart('/') }.Concatenate("/");
        }
    }
}