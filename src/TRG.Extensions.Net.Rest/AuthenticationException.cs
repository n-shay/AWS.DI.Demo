namespace TRG.Extensions.Net.Rest
{
    using System;

    public class AuthenticationException : RestException
    {
        public AuthenticationException(string message) : base(message)
        {
        }

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}