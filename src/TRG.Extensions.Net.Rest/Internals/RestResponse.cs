namespace TRG.Extensions.Net.Rest.Internals
{
    using System;

    internal class RestResponse : IRestResponse
    {
        public string Data { get; }
        public ResponseStatus Status { get; }
        public Exception Exception { get; }

        public RestResponse(string data)
        {
            this.Data = data;
            this.Status = ResponseStatus.Completed;
        }

        public RestResponse(Exception exception)
        {
            this.Status = ResponseStatus.Error;
            this.Exception = exception;
        }

        public static RestResponse Failed(Exception exception) => new RestResponse(exception);
    }

    internal class RestResponse<T> : IRestResponse<T>
    {
        public T Data { get; }
        public ResponseStatus Status { get; }
        public Exception Exception { get; }

        public RestResponse(T data)
        {
            this.Data = data;
            this.Status = ResponseStatus.Completed;
        }

        private RestResponse(Exception exception)
        {
            this.Status = ResponseStatus.Error;
            this.Exception = exception;
        }

        public static RestResponse<T> Failed(Exception exception) => new RestResponse<T>(exception);
    }
}