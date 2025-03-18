using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout
{
    public class OAuthSdkCredentialsTests
    {
        private OAuthSdkCredentials CreateSdkCredentials(HttpResponseMessage mockResponse)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse)
                .Verifiable();

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://fake-auth.com")
            };

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient())
                .Returns(httpClient);

            return new OAuthSdkCredentials(
                httpClientFactoryMock.Object,
                new Uri("https://fake-auth.com"),
                "test_client_id",
                "test_client_secret",
                new HashSet<OAuthScope>()
            );
        }

        [Fact]
        public void ShouldReturnAuthorizationHeaderWhenTokenIsValid()
        {
            using var mockResponse = new HttpResponseMessage();
            mockResponse.StatusCode = HttpStatusCode.OK;
            mockResponse.Content = new StringContent(
                "{\"access_token\": \"valid_token\", \"token_type\": \"Bearer\", \"expires_in\": 3600}",
                Encoding.UTF8,
                "application/json");
            var sdk = CreateSdkCredentials(mockResponse);
            sdk.InitAccess();

            var authorization = sdk.GetSdkAuthorization(SdkAuthorizationType.OAuth);
            Assert.NotNull(authorization);

            string expectedHeader = $"Bearer valid_token";
            string actualHeader = authorization.GetAuthorizationHeader();

            Assert.Equal(expectedHeader, actualHeader);
        }

        [Fact]
        public void ShouldThrowExceptionWhenApiReturnsError()
        {
            using var mockResponse = new HttpResponseMessage();
            mockResponse.StatusCode = HttpStatusCode.BadRequest;
            mockResponse.Content = new StringContent(
                "{\"error\": \"invalid_client\"}",
                Encoding.UTF8,
                "application/json");
            var sdk = CreateSdkCredentials(mockResponse);

            Assert.Throws<CheckoutAuthorizationException>(() => sdk.InitAccess());
        }

        [Fact]
        public void ShouldThrowExceptionWhenResponseHasInvalidToken()
        {
            var response = new OAuthServiceResponse { AccessToken = null, TokenType = "Bearer", ExpiresIn = 3600 };

            Assert.Throws<ArgumentException>(() => OAuthAccessToken.FromOAuthServiceResponse(response));
        }
    }
}