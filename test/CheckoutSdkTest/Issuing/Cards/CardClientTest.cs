using Checkout.Common;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Credentials;
using Checkout.Issuing.Cards.Requests.Enrollment;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Checkout.Issuing.Cards.Responses;
using Checkout.Issuing.Cards.Responses.Credentials;
using Checkout.Issuing.Cards.Responses.Enrollment;
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
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldCreateCard()
        {
            CardVirtualRequest cardRequest = new CardVirtualRequest();
            CardResponse cardResponse = new CardResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardResponse>("issuing/cards", _authorization,
                        cardRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardResponse getResponse = await client.CreateCard(cardRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardResponse);
        }

        [Fact]
        private async Task ShouldGetCardDetails()
        {
            CardDetailsResponse cardDetailsResponse = new VirtualCardDetailsResponse();

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
            PasswordThreeDSEnrollmentRequest cardEnrollThreeDSPasswordRequest = new PasswordThreeDSEnrollmentRequest();
            ThreeDSEnrollmentResponse cardEnrollThreeDSResponse = new ThreeDSEnrollmentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ThreeDSEnrollmentResponse>("issuing/cards/card_id/3ds-enrollment", _authorization,
                        cardEnrollThreeDSPasswordRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardEnrollThreeDSResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            ThreeDSEnrollmentResponse getResponse =
                await client.EnrollCardThreeDS("card_id", cardEnrollThreeDSPasswordRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardEnrollThreeDSResponse);
        }

        [Fact]
        private async Task ShouldUpdateEnrollCardThreeDS()
        {
            ThreeDSUpdateRequest cardEnrollThreeDsDetailsRequest = new ThreeDSUpdateRequest();
            ThreeDSUpdateResponse threeDsUpdateResponse =
                new ThreeDSUpdateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<ThreeDSUpdateResponse>("issuing/cards/card_id/3ds-enrollment",
                        _authorization,
                        cardEnrollThreeDsDetailsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => threeDsUpdateResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            ThreeDSUpdateResponse getResponse =
                await client.UpdateCardThreeDSDetails("card_id", cardEnrollThreeDsDetailsRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(threeDsUpdateResponse);
        }

        [Fact]
        private async Task ShouldGetEnrollCardThreeDS()
        {
            ThreeDSEnrollmentDetailsResponse threeDsEnrollmentDetailsResponse =
                new ThreeDSEnrollmentDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ThreeDSEnrollmentDetailsResponse>("issuing/cards/card_id/3ds-enrollment",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => threeDsEnrollmentDetailsResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            ThreeDSEnrollmentDetailsResponse response =
                await client.GetCardThreeDSDetails("card_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(threeDsEnrollmentDetailsResponse);
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
            RevokeCardRequest cardReasonRequest = new RevokeCardRequest();
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
            SuspendCardRequest cardReasonRequest = new SuspendCardRequest();
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