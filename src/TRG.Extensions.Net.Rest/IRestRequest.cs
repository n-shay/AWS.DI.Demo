namespace TRG.Extensions.Net.Rest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if !NET35
    using System.Threading.Tasks;
#endif

    public interface IRestRequest : IDisposable
    {
        string Url { get; set; }

        HttpVerb Method { get; set; }

        string ContentType { get; set; }

        ICollection<NameValuePair> Headers { get; }

        ICollection<NameValuePair> Parameters { get; }

        ISerializer Serializer { get; set; }

        IAuthenticator Authenticator { get; set; }

        Stream ContentStream { get; }

        void SetContent(string content);

        IRestResponse<T> Execute<T>();

        IRestResponse Execute();

#if !NET35
        Task SetContentAsync(string content);

        Task<IRestResponse<T>> ExecuteAsync<T>();

        Task<IRestResponse> ExecuteAsync();
#endif
    }
}