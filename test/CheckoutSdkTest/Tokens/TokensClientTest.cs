using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Tokens
{
    public class TokensClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousPk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public TokensClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.PublicKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldRequestCardToken()
        {
            var cardTokenRequest = new CardTokenRequest();
            var cardTokenResponse = new CardTokenResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardTokenResponse>("tokens", _authorization, cardTokenRequest, CancellationToken.None
                        , null))
                .ReturnsAsync(() => cardTokenResponse);

            ITokensClient tokensClient = new TokensClient(_apiClient.Object, _configuration.Object);

            var response = await tokensClient.Request(cardTokenRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRequestGooglePayToken()
        {
            var googlePayTokenRequest = new GooglePayTokenRequest();
            var tokenResponse = new TokenResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<TokenResponse>("tokens", _authorization, googlePayTokenRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => tokenResponse);

            ITokensClient tokensClient = new TokensClient(_apiClient.Object, _configuration.Object);

            var response = await tokensClient.Request(googlePayTokenRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRequestApplePayToken()
        {
            var applePayTokenRequest = new ApplePayTokenRequest();
            var tokenResponse = new TokenResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<TokenResponse>("tokens", _authorization, applePayTokenRequest, CancellationToken.None
                        , null))
                .ReturnsAsync(() => tokenResponse);

            ITokensClient tokensClient = new TokensClient(_apiClient.Object, _configuration.Object);

            var response = await tokensClient.Request(applePayTokenRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}