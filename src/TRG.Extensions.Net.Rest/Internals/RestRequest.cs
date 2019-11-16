namespace TRG.Extensions.Net.Rest.Internals
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
#if !NET35
    using System.Threading.Tasks;
#endif

    internal class RestRequest : IRestRequest
    {
        private readonly MemoryStream contentStream;

        public string Url { get; set; }

        public HttpVerb Method { get; set; }

        public string ContentType { get; set; }

        public ICollection<NameValuePair> Headers { get; }

        public ICollection<NameValuePair> Parameters { get; }

        public ISerializer Serializer { get; set; }

        public IAuthenticator Authenticator { get; set; }

        public Stream ContentStream => this.contentStream;

        public RestRequest()
        {
            this.contentStream = new MemoryStream();
            this.Headers = new List<NameValuePair>();
            this.Parameters = new List<NameValuePair>();
        }

        public void SetContent(string content)
        {
            var contentBytes = Encoding.ASCII.GetBytes(content);
            this.ContentStream.Write(contentBytes, 0, contentBytes.Length);
        }

        public IRestResponse<T> Execute<T>()
        {
            if (this.Serializer == null)
                throw new RestException("Serializer is required.");

            var execResult = this.ExecuteInternal();
            if (!execResult.Success)
                return RestResponse<T>.Failed(execResult.Exception);

            try
            {
                var result = execResult.Content != null
                    ? this.Serializer.Deserialize<T>(execResult.Content)
                    : default;

                return new RestResponse<T>(result);
            }
            catch (Exception ex)
            {
                throw new RestException("Failed to deserialize response content.", ex);
            }
        }

        public IRestResponse Execute()
        {
            var execResult = this.ExecuteInternal();

            return execResult.Success
                ? new RestResponse(execResult.Content)
                : RestResponse.Failed(execResult.Exception);
        }

#if !NET35
        public async Task SetContentAsync(string content)
        {
            var contentBytes = Encoding.ASCII.GetBytes(content);
            await this.ContentStream.WriteAsync(contentBytes, 0, contentBytes.Length);
        }

        public async Task<IRestResponse<T>> ExecuteAsync<T>()
        {
            if (this.Serializer == null)
                throw new RestException("Serializer is required.");

            var execResult = await this.ExecuteInternalAsync();
            if (!execResult.Success)
                return RestResponse<T>.Failed(execResult.Exception);

            try
            {
                var result = execResult.Content != null
                    ? this.Serializer.Deserialize<T>(execResult.Content)
                    : default;

                return new RestResponse<T>(result);
            }
            catch (Exception ex)
            {
                throw new RestException("Failed to deserialize response content.", ex);
            }
        }

        public async Task<IRestResponse> ExecuteAsync()
        {
            var execResult = await this.ExecuteInternalAsync();

            return execResult.Success
                ? new RestResponse(execResult.Content)
                : RestResponse.Failed(execResult.Exception);
        }
#endif

        public void Dispose()
        {
            this.ContentStream.Dispose();
        }

#if !NET35
        private async Task<WebRequest> CreateWebRequestInternalAsync()
        {
            try
            {
                var requestUrl = this.Url;

                if (this.Parameters.Any())
                {
                    var parameters = string.Join(
                        "&",
                        this.Parameters.Where(p => !p.IsEmpty)
                            .Select(p => $"{p.Name}={Encode(p.Value)}"));

                    if (this.Method == HttpVerb.Get)
                        requestUrl += (requestUrl.Contains('?') ? "&" : "?") + parameters;
                    else if (this.ContentStream.Length == 0)
                    {
                        await this.SetContentAsync(parameters);
                    }
                }

                var request = WebRequest.Create(requestUrl);
                request.Method = this.Method.ToString().ToUpper();

                if (this.ContentType != null)
                    request.ContentType = this.ContentType;

                if (this.Method != HttpVerb.Get && this.ContentStream.Length > 0)
                {
                    request.ContentLength = this.ContentStream.Length;

                    using (var requestStream = await request.GetRequestStreamAsync())
                    {
                        this.ContentStream.Position = 0;
                        await this.ContentStream.CopyToAsync(requestStream);
                    }
                }
                
                foreach (var header in this.Headers)
                {
                    request.Headers.Add(header.Name, header.Value);
                }

                return request;
            }
            catch (Exception ex)
            {
                throw new RestException("Failed to create WebRequest.", ex);
            }
        }

        private async Task<ExecuteResult> ExecuteInternalAsync()
        {
            this.Authenticator?.Authenticate(this);

            var request = await this.CreateWebRequestInternalAsync();
            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var sr = new StreamReader(responseStream))
                            {
                                var content = await sr.ReadToEndAsync();

                                return new ExecuteResult(content);
                            }
                        }

                        return new ExecuteResult();
                    }
                }
            }
            catch (Exception ex)
            {
                return new ExecuteResult(ex);
            }
        }
#endif

        private WebRequest CreateWebRequestInternal()
        {
            try
            {
                var requestUrl = this.Url;

                if (this.Parameters.Any())
                {
                    var parameters = string.Join(
                        "&",
                        this.Parameters.Where(p => !p.IsEmpty)
                            .Select(p => $"{p.Name}={Encode(p.Value)}")
                            .ToArray());

                    if (this.Method == HttpVerb.Get)
                        requestUrl += (requestUrl.Contains('?') ? "&" : "?") + parameters;
                    else if (this.ContentStream.Length == 0)
                    {
                        this.SetContent(parameters);
                    }
                }

                var request = WebRequest.Create(requestUrl);
                request.Method = this.Method.ToString().ToUpper();

                if (this.ContentType != null)
                    request.ContentType = this.ContentType;

                if (this.Method != HttpVerb.Get && this.ContentStream.Length > 0)
                {
                    request.ContentLength = this.ContentStream.Length;

                    using (var requestStream = request.GetRequestStream())
                    {
                        this.ContentStream.Position = 0;
#if NET35
                        CopyTo(ContentStream, requestStream);
#else
                        this.ContentStream.CopyTo(requestStream);
#endif
                    }
                }

                // combine headers
                foreach (var header in this.Headers)
                {
                    request.Headers.Add(header.Name, header.Value);
                }

                return request;
            }
            catch (Exception ex)
            {
                throw new RestException("Failed to create WebRequest.", ex);
            }
        }

        private ExecuteResult ExecuteInternal()
        {
            this.Authenticator?.Authenticate(this);

            var request = this.CreateWebRequestInternal();
            try
            {
                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var sr = new StreamReader(responseStream))
                            {
                                var content = sr.ReadToEnd();

                                return new ExecuteResult(content);
                            }
                        }

                        return new ExecuteResult();
                    }
                }
            }
            catch (Exception ex)
            {
                return new ExecuteResult(ex);
            }
        }

        private static string Encode(string value)
        {
            return Uri.EscapeDataString(value);
        }

#if NET35
        private const int StreamCopyBufferSize = 16384;

        private static void CopyTo(Stream input, Stream output)
        {
            var buffer = new byte[StreamCopyBufferSize]; // Fairly arbitrary size
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
#endif

        private class ExecuteResult
        {
            public ExecuteResult(string content = null)
            {
                this.Success = true;
                this.Content = content;
            }

            public ExecuteResult(Exception exception)
            {
                this.Success = false;
                this.Exception = exception;
            }

            public bool Success { get; }

            public string Content { get; }

            public Exception Exception { get; }
        }

    }
}