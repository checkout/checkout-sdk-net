using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cardholders
{
    public class CardholdersClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CardholdersClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldCreateCardholder()
        {
            CardholderRequest cardholderRequest = new CardholderRequest();
            CardholderResponse cardholderResponse = new CardholderResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardholderResponse>("issuing/cardholders", _authorization,
                        cardholderRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardholderResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            CardholderResponse response = await client.CreateCardholder(cardholderRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardholderResponse);
        }

        [Fact]
        private async Task ShouldGetCardholderDetails()
        {
            CardholderDetailsResponse cardholderDetailsesResponse = new CardholderDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<CardholderDetailsResponse>(
                        "issuing/cardholders/cardholder_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => cardholderDetailsesResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardholderDetailsResponse response = await client.GetCardholderDetails("cardholder_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardholderDetailsesResponse);
        }
        
        [Fact]
        private async Task ShouldGetCardholderCards()
        {
            CardholderCardsResponse cardholderCardsResponse = new CardholderCardsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<CardholderCardsResponse>(
                        "issuing/cardholders/cardholder_id/cards",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => cardholderCardsResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardholderCardsResponse response = await client.GetCardholdersCards("cardholder_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardholderCardsResponse);
        }
    }
}