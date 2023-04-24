using Checkout.Common;
using Checkout.Issuing.Cards.Enroll;
using Checkout.Issuing.Cards.Type;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Cards
{
    public class CardClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CardClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldCreateCard()
        {
            CardTypeVirtualRequest cardTypeRequest = new CardTypeVirtualRequest();
            CardResponse cardResponse = new CardResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardResponse>("issuing/cards", _authorization,
                        cardTypeRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardResponse getResponse = await client.CreateCard(cardTypeRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardResponse);
        }

        [Fact]
        private async Task ShouldGetCardDetails()
        {
            CardDetailsResponse cardDetailsResponse = new CardDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<CardDetailsResponse>(
                        "issuing/cards/card_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => cardDetailsResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardDetailsResponse getResponse = await client.GetCardDetails("card_id", CancellationToken.None);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardDetailsResponse);
        }

        [Fact]
        private async Task ShouldEnrollCardThreeDS()
        {
            CardEnrollThreeDSPasswordRequest cardEnrollThreeDSPasswordRequest = new CardEnrollThreeDSPasswordRequest();
            CardEnrollThreeDSResponse cardEnrollThreeDSResponse = new CardEnrollThreeDSResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardEnrollThreeDSResponse>("issuing/cards/card_id/3ds-enrollment", _authorization,
                        cardEnrollThreeDSPasswordRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardEnrollThreeDSResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardEnrollThreeDSResponse getResponse =
                await client.EnrollCardThreeDS("card_id", cardEnrollThreeDSPasswordRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardEnrollThreeDSResponse);
        }

        [Fact]
        private async Task ShouldUpdateEnrollCardThreeDS()
        {
            CardEnrollThreeDSDetailsRequest cardEnrollThreeDsDetailsRequest = new CardEnrollThreeDSDetailsRequest();
            CardEnrollThreeDSDetailsUpdateResponse cardEnrollThreeDSDetailsUpdateResponse =
                new CardEnrollThreeDSDetailsUpdateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<CardEnrollThreeDSDetailsUpdateResponse>("issuing/cards/card_id/3ds-enrollment",
                        _authorization,
                        cardEnrollThreeDsDetailsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardEnrollThreeDSDetailsUpdateResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardEnrollThreeDSDetailsUpdateResponse getResponse =
                await client.UpdateCardThreeDSDetails("card_id", cardEnrollThreeDsDetailsRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardEnrollThreeDSDetailsUpdateResponse);
        }

        [Fact]
        private async Task ShouldGetEnrollCardThreeDS()
        {
            CardEnrollThreeDSDetailsGetResponse cardEnrollThreeDSDetailsGetResponse =
                new CardEnrollThreeDSDetailsGetResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<CardEnrollThreeDSDetailsGetResponse>("issuing/cards/card_id/3ds-enrollment",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => cardEnrollThreeDSDetailsGetResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardEnrollThreeDSDetailsGetResponse getResponse =
                await client.GetCardThreeDSDetails("card_id");

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardEnrollThreeDSDetailsGetResponse);
        }

        [Fact]
        private async Task ShouldActivateCard()
        {
            Resource response = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>("issuing/cards/card_id/activate",
                        _authorization,
                        null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => response);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource getResponse = await client.ActivateCard("card_id");

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(response);
        }

        [Fact]
        private async Task ShouldGetCardCredentials()
        {
            CardCredentialsQuery cardCredentialsQuery = new CardCredentialsQuery();
            CardCredentialsResponse cardCredentialsResponse =
                new CardCredentialsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<CardCredentialsResponse>("issuing/cards/card_id/credentials",
                        _authorization,
                        cardCredentialsQuery,
                        CancellationToken.None))
                .ReturnsAsync(() => cardCredentialsResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardCredentialsResponse getResponse = await client.GetCardCredentials("card_id", cardCredentialsQuery);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardCredentialsResponse);
        }

        [Fact]
        private async Task ShouldRevokeCard()
        {
            CardReasonRequest cardReasonRequest = new CardReasonRequest();
            Resource ResourceResponse = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>("issuing/cards/card_id/revoke",
                        _authorization,
                        cardReasonRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => ResourceResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource getResponse = await client.RevokeCard("card_id", cardReasonRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(ResourceResponse);
        }

        [Fact]
        private async Task ShouldSuspendCard()
        {
            CardReasonRequest cardReasonRequest = new CardReasonRequest();
            Resource resourceResponse = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>("issuing/cards/card_id/suspend",
                        _authorization,
                        cardReasonRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => resourceResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource getResponse = await client.SuspendCard("card_id", cardReasonRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(resourceResponse);
        }
    }
}