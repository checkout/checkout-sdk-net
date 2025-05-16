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
            AbstractCardControlRequest abstractCardControlRequest = new VelocityCardControlRequest();
            AbstractCardControlResponse abstractCardControlVelocityLimitResponse =
                new VelocityCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AbstractCardControlResponse>("issuing/controls", _authorization,
                        abstractCardControlRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => abstractCardControlVelocityLimitResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            AbstractCardControlResponse response = await client.CreateCardControl(abstractCardControlRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(abstractCardControlVelocityLimitResponse);
        }

        [Fact]
        private async Task ShouldCreateCardMccLimitControl()
        {
            AbstractCardControlRequest abstractCardControlRequest = new MccCardControlRequest();
            AbstractCardControlResponse abstractCardControlMccLimitResponse = new MccCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AbstractCardControlResponse>("issuing/controls", _authorization,
                        abstractCardControlRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => abstractCardControlMccLimitResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            AbstractCardControlResponse response = await client.CreateCardControl(abstractCardControlRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(abstractCardControlMccLimitResponse);
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
            AbstractCardControlResponse abstractCardControlResponse = new VelocityCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<AbstractCardControlResponse>("issuing/controls/control_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => abstractCardControlResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            AbstractCardControlResponse response = await client.GetCardControlDetails("control_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(abstractCardControlResponse);
        }

        [Fact]
        private async Task ShouldUpdateCardControl()
        {
            AbstractCardControlUpdate cardControlUpdate = new VelocityCardControlUpdate();
            AbstractCardControlResponse abstractCardControlResponse = new VelocityCardControlResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<AbstractCardControlResponse>("issuing/controls/control_id",
                        _authorization,
                        cardControlUpdate,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => abstractCardControlResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            AbstractCardControlResponse response = await client.UpdateCardControl("control_id", cardControlUpdate);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(abstractCardControlResponse);
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