using Checkout.Common;
using Checkout.Issuing.Controls.Requests.Create;
using Checkout.Issuing.Controls.Requests.Query;
using Checkout.Issuing.Controls.Requests.Update;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Issuing.Controls.Responses.Query;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Controls
{
    public class CardControlsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CardControlsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldCreateCardVelocityLimitControl()
        {
            CardControlRequest cardControlRequest = new VelocityCardControlRequest();
            CardControlResponse cardControlVelocityLimitResponse =
                new VelocityCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardControlResponse>("issuing/controls", _authorization,
                        cardControlRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardControlVelocityLimitResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            CardControlResponse response = await client.CreateCardControl(cardControlRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardControlVelocityLimitResponse);
        }

        [Fact]
        private async Task ShouldCreateCardMccLimitControl()
        {
            CardControlRequest cardControlRequest = new MccCardControlRequest();
            CardControlResponse cardControlMccLimitResponse = new MccCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardControlResponse>("issuing/controls", _authorization,
                        cardControlRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardControlMccLimitResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            CardControlResponse response = await client.CreateCardControl(cardControlRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardControlMccLimitResponse);
        }

        [Fact]
        private async Task ShouldGetCardsControl()
        {
            CardControlQueryTarget cardControlQueryTarget = new CardControlQueryTarget();
            CardControlsQueryResponse cardControlsQueryResponse = new CardControlsQueryResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<CardControlsQueryResponse>("issuing/controls",
                        _authorization,
                        cardControlQueryTarget,
                        CancellationToken.None))
                .ReturnsAsync(() => cardControlsQueryResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            CardControlsQueryResponse response = await client.GetCardControls(cardControlQueryTarget);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardControlsQueryResponse);
        }

        [Fact]
        private async Task ShouldGetCardControlDetails()
        {
            CardControlResponse cardControlResponse = new VelocityCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<CardControlResponse>("issuing/controls/control_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => cardControlResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            CardControlResponse response = await client.GetCardControlDetails("control_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardControlResponse);
        }

        [Fact]
        private async Task ShouldUpdateCardControl()
        {
            UpdateCardControlRequest updateCardControlRequest = new UpdateCardControlRequest();
            CardControlResponse cardControlResponse = new VelocityCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<CardControlResponse>("issuing/controls/control_id",
                        _authorization,
                        updateCardControlRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => cardControlResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            CardControlResponse response = await client.UpdateCardControl("control_id", updateCardControlRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(cardControlResponse);
        }

        [Fact]
        private async Task ShouldDeleteCardControl()
        {
            IdResponse removeCardControlResponse = new IdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<IdResponse>("issuing/controls/control_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => removeCardControlResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            IdResponse response = await client.RemoveCardControl("control_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(removeCardControlResponse);
        }
    }
}