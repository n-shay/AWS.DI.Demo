namespace TRG.Extensions.Net.Rest.Test
{
    using Moq;
    using System.Linq;
    using Xunit;

    public class RestClientTests
    {
        private const string BASE_URL = "http://www.google.com";

        [Fact]
        public void ValidateRequestBuilder_Url()
        {
            const string relativeUrl = "some/api";

            var expectedRequestUrl = $"{BASE_URL}/{relativeUrl}";

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.WithUrl(relativeUrl)
                .Build();

            // Assert
            Assert.Equal(expectedRequestUrl, request.Url);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ValidateRequestExecution()
        {
            const int returnedValue = 555;

            // Arrange
            var mockSerializer = new Mock<ISerializer>();
            mockSerializer.Setup(s => s.Deserialize<int>(It.IsAny<string>())).Returns(returnedValue);
            var client = new RestClient(BASE_URL)
                .SetSerializer(mockSerializer.Object);

            // Act
            var builder = client.CreateRequest();
            IRestResponse<int> response;
            using (var request = builder.Build())
            {
                 response = request.Execute<int>();
            }

            // Assert
            Assert.Equal(ResponseStatus.Completed, response.Status);
            mockSerializer.Verify(s => s.Deserialize<int>(It.Is<string>(v => !string.IsNullOrWhiteSpace(v))), Times.Once);
            Assert.Equal(returnedValue, response.Data);
        }

        [Fact]
        public void ValidateRequestBuilder_NoMethodSet()
        {
            const HttpVerb defaultMethod = HttpVerb.Get;

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.Build();

            // Assert
            Assert.Equal(defaultMethod, request.Method);
        }

        [Fact]
        public void ValidateRequestBuilder_AsGet()
        {
            const HttpVerb expectedMethod = HttpVerb.Get;

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AsGet()
                .Build();

            // Assert
            Assert.Equal(expectedMethod, request.Method);
        }

        [Fact]
        public void ValidateRequestBuilder_AsPost()
        {
            const HttpVerb expectedMethod = HttpVerb.Post;

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AsPost()
                .Build();

            // Assert
            Assert.Equal(expectedMethod, request.Method);
        }

        [Fact]
        public void ValidateRequestBuilder_AsMethod()
        {
            const HttpVerb expectedMethod = HttpVerb.Patch;

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AsMethod(expectedMethod)
                .Build();

            // Assert
            Assert.Equal(expectedMethod, request.Method);
        }

        [Fact]
        public void ValidateRequestBuilder_AddHeader()
        {
            const string headerName = "Accept";
            const string headerValue = "application/json";

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AddHeader(headerName, headerValue)
                .Build();

            // Assert
            Assert.Single(request.Headers);
            Assert.Contains(request.Headers, nvp => nvp.Name == headerName && nvp.Value == headerValue);
        }

        [Fact]
        public void ValidateRequestBuilder_AddHeader_WithDefault()
        {
            const string headerName = "Accept";
            const string defaultHeaderValue = "application/json";
            const string headerValue = "text/plain";

            // Arrange
            var client = new RestClient(BASE_URL)
                .AddDefaultHeader(headerName, defaultHeaderValue);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AddHeader(headerName, headerValue)
                .Build();

            // Assert
            Assert.Single(request.Headers);
            Assert.Contains(request.Headers, nvp => nvp.Name == headerName && nvp.Value == headerValue);
        }

        [Fact]
        public void ValidateRequestBuilder_AddParameter()
        {
            const string paramName = "SomeArg";
            const string paramValue = "test123";

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AddParameter(paramName, paramValue)
                .Build();

            // Assert
            Assert.Single(request.Parameters);
            Assert.Contains(request.Parameters, nvp => nvp.Name == paramName && nvp.Value == paramValue);
        }

        [Fact]
        public void ValidateRequestBuilder_AddParameterTwice()
        {
            const string paramName = "SomeArg";
            const string paramValue1 = "test123";
            const string paramValue2 = "test124";

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.AddParameter(paramName, paramValue1)
                .AddParameter(paramName, paramValue2)
                .Build();

            // Assert
            Assert.Equal(2, request.Parameters.Count());
            Assert.Contains(request.Parameters, nvp => nvp.Name == paramName && nvp.Value == paramValue1);
            Assert.Contains(request.Parameters, nvp => nvp.Name == paramName && nvp.Value == paramValue2);
        }

        [Fact]
        public void ValidateRequestBuilder_WithContentType()
        {
            const string contentType = "text/plain";

            // Arrange
            IRestClient client = new RestClient(BASE_URL);

            // Act
            var builder = client.CreateRequest();
            var request = builder.WithContentType(contentType)
                .Build();

            // Assert
            Assert.Equal(contentType, request.ContentType);
        }

        [Fact]
        public void ValidateClient_SetDefaultContentType()
        {
            const string defaultContentType = "text/plain";

            // Arrange
            var client = new RestClient(BASE_URL)
                .SetDefaultContentType(defaultContentType);

            // Act
            var builder = client.CreateRequest();
            var request = builder.Build();

            // Assert
            Assert.Equal(defaultContentType, request.ContentType);
        }

        [Fact]
        public void ValidateClient_AddDefaultHeader()
        {
            const string defaultHeaderName = "Accept";
            const string defaultHeaderValue = "application/json";

            // Arrange
            var client = new RestClient(BASE_URL)
                .AddDefaultHeader(defaultHeaderName, defaultHeaderValue);

            // Act
            var builder = client.CreateRequest();
            var request = builder.Build();

            // Assert
            Assert.Single(request.Headers);
            Assert.Contains(request.Headers, nvp => nvp.Name == defaultHeaderName && nvp.Value == defaultHeaderValue);
        }

        [Fact]
        public void ValidateClient_SetSerializer()
        {
            // Arrange
            var mockSerializer = new Mock<ISerializer>();
            var client = new RestClient(BASE_URL)
                .SetSerializer(mockSerializer.Object);

            // Act
            var builder = client.CreateRequest();
            var request = builder.Build();

            // Assert
            Assert.Same(mockSerializer.Object, request.Serializer);
        }

        [Fact]
        public void ValidateClient_SetAuthenticator()
        {
            // Arrange
            var mockAuthenticator = new Mock<IAuthenticator>();
            var client = new RestClient(BASE_URL)
                .SetAuthenticator(mockAuthenticator.Object);

            // Act
            var builder = client.CreateRequest();
            var request = builder.Build();

            // Assert
            Assert.NotNull(request.Authenticator);
        }


    }
}
