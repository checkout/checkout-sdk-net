using Checkout.Common;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Credentials;
using Checkout.Issuing.Cards.Requests.Enrollment;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Cards.Requests.Renew;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Cards.Responses.Credentials;
using Checkout.Issuing.Cards.Responses.Enrollment;
using Checkout.Issuing.Cards.Responses.Renew;
using Checkout.Issuing.Common.Responses;
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
            VirtualCardCreateRequest abstractCardCreateRequest = new VirtualCardCreateRequest();
            AbstractCardCreateResponse abstractCardResponse = new VirtualCardCreateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AbstractCardCreateResponse>("issuing/cards", _authorization,
                        abstractCardCreateRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => abstractCardResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            AbstractCardCreateResponse getResponse = await client.CreateCard(abstractCardCreateRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(abstractCardResponse);
        }

        [Fact]
        private async Task ShouldGetCardDetails()
        {
            AbstractCardResponse cardDetailsResponse = new VirtualCardResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<AbstractCardResponse>(
                        "issuing/cards/card_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => cardDetailsResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            AbstractCardResponse getResponse = await client.GetCardDetails("card_id", CancellationToken.None);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardDetailsResponse);
        }

        [Fact]
        private async Task ShouldEnrollCardThreeDS()
        {
            PasswordThreeDsEnrollmentRequest cardEnrollThreeDsPasswordRequest = new PasswordThreeDsEnrollmentRequest();
            ThreeDsEnrollmentResponse cardEnrollThreeDSResponse = new ThreeDsEnrollmentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ThreeDsEnrollmentResponse>("issuing/cards/card_id/3ds-enrollment", _authorization,
                        cardEnrollThreeDsPasswordRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardEnrollThreeDSResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            ThreeDsEnrollmentResponse getResponse =
                await client.EnrollCardThreeDS("card_id", cardEnrollThreeDsPasswordRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardEnrollThreeDSResponse);
        }

        [Fact]
        private async Task ShouldUpdateEnrollCardThreeDS()
        {
            SecurityQuestionThreeDsUpdateRequest cardEnrollSecurityQuestionThreeDsDetailsRequest = new SecurityQuestionThreeDsUpdateRequest();
            ThreeDsEnrollmentUpdateResponse threeDsEnrollmentUpdateResponse =
                new ThreeDsEnrollmentUpdateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<ThreeDsEnrollmentUpdateResponse>("issuing/cards/card_id/3ds-enrollment",
                        _authorization,
                        cardEnrollSecurityQuestionThreeDsDetailsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => threeDsEnrollmentUpdateResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            ThreeDsEnrollmentUpdateResponse getResponse =
                await client.UpdateCardThreeDSDetails("card_id", cardEnrollSecurityQuestionThreeDsDetailsRequest);

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(threeDsEnrollmentUpdateResponse);
        }

        [Fact]
        private async Task ShouldGetEnrollCardThreeDS()
        {
            ThreeDsEnrollmentDetailsResponse threeDsEnrollmentDetailsResponse =
                new ThreeDsEnrollmentDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ThreeDsEnrollmentDetailsResponse>("issuing/cards/card_id/3ds-enrollment",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => threeDsEnrollmentDetailsResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            ThreeDsEnrollmentDetailsResponse response =
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

        [Fact]
        private async Task ShouldUpdateCardDetails()
        {
            var cardUpdateRequest = new CardsUpdateRequest();
            var updateResponse = new UpdateResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<UpdateResponse>(
                        "issuing/cards/card_id",
                        _authorization,
                        cardUpdateRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => updateResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            UpdateResponse response = await client.UpdateCardDetails("card_id", cardUpdateRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(updateResponse);
        }

        [Fact]
        private async Task ShouldRenewCard()
        {
            var renewRequest = new VirtualCardRenewRequest();
            var renewResponse = new RenewCardResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RenewCardResponse>(
                        "issuing/cards/card_id/renew",
                        _authorization,
                        renewRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => renewResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            RenewCardResponse response = await client.RenewCard("card_id", renewRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(renewResponse);
        }

        [Fact]
        private async Task ShouldScheduleCardRevocation()
        {
            var scheduleRequest = new ScheduleCardRevocationRequest();
            var resourceResponse = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>(
                        "issuing/cards/schedule-revocation",
                        _authorization,
                        scheduleRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => resourceResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource response = await client.ScheduleCardRevocation(scheduleRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(resourceResponse);
        }

        [Fact]
        private async Task ShouldDeleteScheduledRevocation()
        {
            var resourceResponse = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<Resource>(
                        "issuing/cards/card_id/schedule-revocation",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => resourceResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource response = await client.DeleteScheduledRevocation("card_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(resourceResponse);
        }
    }
}