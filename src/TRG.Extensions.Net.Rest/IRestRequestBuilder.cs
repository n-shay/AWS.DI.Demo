namespace TRG.Extensions.Net.Rest
{
    public interface IRestRequestBuilder
    {
        /// <summary>
        /// Sets the HTTP method.
        /// </summary>
        IRestRequestBuilder AsMethod(HttpVerb method);

        /// <summary>
        /// Shorthand for <code>AsMethod(HttpVerb.Post)</code>.
        /// </summary>
        IRestRequestBuilder AsPost();

        /// <summary>
        /// Shorthand for <code>AsMethod(HttpVerb.Get)</code>.
        /// </summary>
        IRestRequestBuilder AsGet();
        
        IRestRequestBuilder AddHeader(string name, string value);

        IRestRequestBuilder AddParameter(string name, string value);

        IRestRequestBuilder WithContentType(string contentType);

        IRestRequestBuilder WithUrl(string relativeUrl);

        IRestRequest Build();
    }
}