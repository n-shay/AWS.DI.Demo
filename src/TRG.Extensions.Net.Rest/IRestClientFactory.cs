namespace TRG.Extensions.Net.Rest
{
    public interface IRestClientFactory
    {
        IRestClient Create(string baseUrl);
    }
}