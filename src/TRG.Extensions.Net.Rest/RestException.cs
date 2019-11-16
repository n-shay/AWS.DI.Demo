namespace TRG.Extensions.Net.Rest
{
    using System;

    public class RestException : Exception
    {
        public RestException(string message) : base(message)
        {
        }

        public RestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}