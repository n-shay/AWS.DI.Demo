namespace TRG.Extensions.Net.Rest
{
    public interface IAuthenticator
    {
        void Authenticate(IRestRequest request);
    }
}