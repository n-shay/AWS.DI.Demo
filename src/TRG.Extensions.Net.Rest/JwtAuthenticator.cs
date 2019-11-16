namespace TRG.Extensions.Net.Rest
{
    using System;
    using System.Linq;

    public class JwtAuthenticator : IAuthenticator
    {
        private readonly string authHeader;

        public JwtAuthenticator(string accessToken)
        {
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));

            this.authHeader = $"Bearer {accessToken}";
        }

        public void Authenticate(IRestRequest request)
        {
            if (request.Headers.All(h => h.Name != "Authorization"))
                request.Headers.Add(new NameValuePair("Authorization", this.authHeader));
        }
    }
}