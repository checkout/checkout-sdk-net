using Checkout.Issuing.Testing.Requests;
using Checkout.Issuing.Testing.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Testing
{
    public class CardTestingClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CardTestingClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldSimulateAuthorization()
        {
            CardAuthorizationRequest cardAuthorizationRequest = new CardAuthorizationRequest();
            CardAuthorizationResponse cardAuthorizationResponse = new CardAuthorizationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardAuthorizationResponse>("issuing/simulate/authorizations", _authorization,
                        cardAuthorizationRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardAuthorizationResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardAuthorizationResponse getResponse = await client.SimulateAuthorization(cardAuthorizationRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardAuthorizationResponse);
        }
    }
}