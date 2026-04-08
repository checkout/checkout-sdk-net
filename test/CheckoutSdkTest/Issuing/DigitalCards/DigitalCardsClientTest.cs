using Checkout.Issuing;
using Checkout.Issuing.DigitalCards.Responses;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.DigitalCards
{
    public class DigitalCardsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public DigitalCardsClientTest()
        {
            _sdkCredentials.Setup(c => c.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task GetDigitalCard_WhenIdIsValid_ShouldCallApiClientGet()
        {
            const string digitalCardId = "dcr_5ngxzsynm2me3oxf73esbhda6q";
            var expectedResponse = new GetDigitalCardResponse
            {
                Id = digitalCardId,
                CardId = "crd_test123",
                ClientId = "cli_test123",
                EntityId = "ent_test123",
                LastFour = "4242",
                Status = IssuingDigitalCardStatus.Active,
                Type = IssuingDigitalCardType.SecureElement,
                ProvisionedOn = new DateTime(2025, 1, 1)
            };

            _apiClient.Setup(c => c.Get<GetDigitalCardResponse>(
                    $"issuing/digital-cards/{digitalCardId}",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetDigitalCard(digitalCardId);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(digitalCardId);
            response.LastFour.ShouldBe("4242");
            response.Status.ShouldBe(IssuingDigitalCardStatus.Active);
        }

        [Fact]
        public async Task GetDigitalCard_WhenIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetDigitalCard(null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetDigitalCard_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            const string digitalCardId = "dcr_5ngxzsynm2me3oxf73esbhda6q";
            var cancellationToken = new CancellationToken();
            var expectedResponse = new GetDigitalCardResponse { Id = digitalCardId };

            _apiClient.Setup(c => c.Get<GetDigitalCardResponse>(
                    $"issuing/digital-cards/{digitalCardId}",
                    _authorization,
                    cancellationToken))
                .ReturnsAsync(expectedResponse);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetDigitalCard(digitalCardId, cancellationToken);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(digitalCardId);
        }
    }
}
