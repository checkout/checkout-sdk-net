using Checkout.Issuing.CardholderAccessTokens.Requests;
using Checkout.Issuing.CardholderAccessTokens.Responses;
using Moq;
using Shouldly;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing
{
    public class CardHolderAccessTokensClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CardHolderAccessTokensClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task RequestCardholderAccessToken_Should_Call_ApiClient_Post()
        {
            var request = new CardholderAccessTokenRequest
            {
                GrantType = "client_credentials",
                ClientId = "client_id",
                ClientSecret = "client_secret",
                CardholderId = "crh_abcdefghijklmnopqrstuvwxyz12",
                SingleUse = true
            };
            var response = new CardholderAccessTokenResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardholderAccessTokenResponse>(
                        "issuing/access/connect/token",
                        _authorization,
                        It.IsAny<FormUrlEncodedContent>(),
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardholderAccessTokenResponse result = await client.RequestCardholderAccessToken(request);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }
    }
}
