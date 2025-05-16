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

        [Fact]
        private async Task ShouldSimulateIncrementingAuthorization()
        {
            CardIncrementAuthorizationRequest cardIncrementAuthorizationRequest =
                new CardIncrementAuthorizationRequest();
            CardIncrementAuthorizationResponse cardIncrementAuthorizationResponse = new CardIncrementAuthorizationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardIncrementAuthorizationResponse>(
                        "issuing/simulate/authorizations/authorization_id/authorizations",
                        _authorization,
                        cardIncrementAuthorizationRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardIncrementAuthorizationResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardIncrementAuthorizationResponse getResponse = await client.SimulateIncrementingAuthorization(
                "authorization_id",
                cardIncrementAuthorizationRequest
            );

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardIncrementAuthorizationResponse);
        }
        
        [Fact]
        private async Task ShouldSimulateClearing()
        {
            CardClearingAuthorizationRequest cardClearingAuthorizationRequest = new CardClearingAuthorizationRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<EmptyResponse>(
                        "issuing/simulate/authorizations/authorization_id/presentments",
                        _authorization,
                        cardClearingAuthorizationRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new EmptyResponse());

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            EmptyResponse getResponse = await client.SimulateClearing(
                "authorization_id",
                cardClearingAuthorizationRequest
            );

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldSimulateReversal()
        {
            CardReversalAuthorizationRequest cardReversalAuthorizationRequest = new CardReversalAuthorizationRequest();
            CardReversalAuthorizationResponse cardReversalAuthorizationResponse = new CardReversalAuthorizationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CardReversalAuthorizationResponse>(
                        "issuing/simulate/authorizations/authorization_id/reversals",
                        _authorization,
                        cardReversalAuthorizationRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => cardReversalAuthorizationResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            CardReversalAuthorizationResponse getResponse = await client.SimulateReversal(
                "authorization_id",
                cardReversalAuthorizationRequest
            );

            getResponse.ShouldNotBeNull();
            getResponse.ShouldBeSameAs(cardReversalAuthorizationResponse);
        }

        [Fact]
        private async Task ShouldSimulateRefund()
        {
            CardRefundAuthorizationRequest cardRefundAuthorizationRequest = new CardRefundAuthorizationRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<EmptyResponse>(
                        "issuing/simulate/authorizations/authorization_id/refunds",
                        _authorization,
                        cardRefundAuthorizationRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new EmptyResponse());

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            EmptyResponse getResponse = await client.SimulateRefund(
                "authorization_id",
                cardRefundAuthorizationRequest
            );

            getResponse.ShouldNotBeNull();
        }
    }
}