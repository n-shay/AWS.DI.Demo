namespace TRG.Extensions.Net.Rest
{
    using System;

    public interface IRestResponse : IRestResponse<string>
    {
    }

    public interface IRestResponse<out T>
    {
        T Data { get; }

        ResponseStatus Status { get; }

        Exception Exception { get; }
    }
}