namespace TRG.Extensions.Net.Rest
{
    public interface IRestClient
    {
        IRestClient SetDefaultContentType(string contentType);

        IRestClient AddDefaultHeader(string name, string value);

        IRestClient SetSerializer(ISerializer serializer);

        IRestClient SetAuthenticator(IAuthenticator authenticator);

        IRestRequestBuilder CreateRequest();
    }
}
