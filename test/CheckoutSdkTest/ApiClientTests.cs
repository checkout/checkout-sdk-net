using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Checkout
{
    public class ApiClientTests : UnitTestFixture, IDisposable
    {
        private readonly ApiClient _apiClient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;

        public ApiClientTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(factory => factory.CreateClient())
                .Returns(_httpClient);

            _apiClient = new ApiClient(httpClientFactoryMock.Object, new Uri("https://api.example.com"), false);
        }

        [Fact]
        public async Task ShouldGetReturnDeserializedObject()
        {
            // Arrange
            var jsonResponse = "{\"message\":\"success\"}";
            using var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponse.Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json");

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);

            // Act
            var result = await _apiClient.Get<FakeResponse>("/test", authorization);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FakeResponse>(result);
            Assert.Equal("success", result.Message);
        }

        [Fact]
        public async Task ShouldPostReturnDeserializedObject()
        {
            // Arrange
            var jsonResponse = "{\"message\":\"created\"}";
            using HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
            var requestData = new { name = "new item" };

            // Act
            var result = await _apiClient.Post<FakeResponse>("/test", authorization, requestData);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FakeResponse>(result);
            Assert.Equal("created", result.Message);
        }

        [Fact]
        public async Task ShouldDeleteReturnDeserializedObject()
        {
            // Arrange
            var jsonResponse = "{\"message\":\"deleted\"}";
            using HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);

            // Act
            var result = await _apiClient.Delete<FakeResponse>("/test", authorization);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FakeResponse>(result);
            Assert.Equal("deleted", result.Message);
        }

        [Fact]
        public async Task ShouldGetHandleNoContentResponse()
        {
            // Arrange
            using var httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent);
            httpResponse.Content = null;

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);

            // Act
            var result = await _apiClient.Get<HttpMetadata>("/test", authorization);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<HttpMetadata>(result);
            Assert.Equal(204, result.HttpStatusCode);
            Assert.NotNull(result.Body);
            Assert.NotNull(result.ResponseHeaders);
        }
        
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }

    public class FakeResponse : HttpMetadata
    {
        public string Message { get; set; }
    }
}