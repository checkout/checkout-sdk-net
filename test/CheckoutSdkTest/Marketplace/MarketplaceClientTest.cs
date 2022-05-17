using Checkout.Common;
using Checkout.Marketplace.Balances;
using Checkout.Marketplace.Payout.Request;
using Checkout.Marketplace.Payout.Response;
using Checkout.Marketplace.Transfer;
using Moq;
using Shouldly;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Marketplace
{
    public class MarketplaceClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Four, ValidDefaultSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Four);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<IApiClient> _apiFilesClient = new Mock<IApiClient>();
        private readonly Mock<IApiClient> _transfersClient = new Mock<IApiClient>();
        private readonly Mock<IApiClient> _balancesClient = new Mock<IApiClient>();
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly MarketplaceClient _marketplaceClient;

        public MarketplaceClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory);
            _marketplaceClient =
                new MarketplaceClient(_apiClient.Object, _apiFilesClient.Object, _transfersClient.Object,
                    _balancesClient.Object, configuration.Object);
        }

        [Fact]
        public async Task ShouldCreateEntity()
        {
            var onboardEntityResponse = new OnboardEntityResponse {Id = "Id"};

            _apiClient.Setup(x => x.Post<OnboardEntityResponse>("marketplace/entities", It.IsAny<SdkAuthorization>(),
                    It.IsAny<object>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(onboardEntityResponse);

            var response = await _marketplaceClient.CreateEntity(new OnboardEntityRequest());

            response.ShouldNotBeNull();
            response.ShouldBe(onboardEntityResponse);
        }

        [Fact]
        private async Task ShouldGetEntity()
        {
            var responseObject = new OnboardEntityDetailsResponse {Id = "entity_id"};

            _apiClient
                .Setup(x =>
                    x.Get<OnboardEntityDetailsResponse>(
                        "marketplace/entities/entity_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseObject);

            var response = await _marketplaceClient.GetEntity(responseObject.Id);

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        private async Task ShouldUpdateEntity()
        {
            var responseObject = new OnboardEntityResponse {Id = "entity_id", Reference = "A"};

            _apiClient
                .Setup(x =>
                    x.Put<OnboardEntityResponse>(
                        "marketplace/entities/entity_id",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(responseObject);

            var response = await _marketplaceClient.UpdateEntity(
                responseObject.Id,
                new OnboardEntityRequest {Reference = "A"});

            response.ShouldNotBeNull();
            response.Id.ShouldBe(responseObject.Id);
            response.Reference.ShouldBe(responseObject.Reference);
        }

        [Fact]
        private async Task ShouldCreatePaymentInstrument()
        {
            _apiClient
                .Setup(x =>
                    x.Post<object>(
                        "marketplace/entities/entity_id/instruments",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(() => new object());

            var response = await _marketplaceClient.CreatePaymentInstrument(
                "entity_id",
                new MarketplacePaymentInstrument {AccountNumber = "324445"});

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldSubmitFile()
        {
            //Arrange
            var responseObject = new IdResponse {Id = "Id"};

            _apiFilesClient
                .Setup(x =>
                    x.Post<IdResponse>(
                        It.IsAny<string>(),
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<MultipartFormDataContent>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseObject);

            var response =
                await _marketplaceClient.SubmitFile(new MarketplaceFileRequest
                {
                    File = "./Resources/checkout.jpeg",
                    ContentType = null,
                    Purpose = MarketplaceFilePurpose.Identification
                });

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        public async Task ShouldInitiateTransferOfFunds()
        {
            var createTransferRequest = new CreateTransferRequest {Reference = "Reference"};
            var createTransferResponse = new CreateTransferResponse();

            _transfersClient.Setup(x => x.Post<CreateTransferResponse>("transfers", It.IsAny<SdkAuthorization>(),
                    createTransferRequest, It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(createTransferResponse);

            var response = await _marketplaceClient.InitiateTransferOfFunds(createTransferRequest);

            response.ShouldNotBeNull();
            response.ShouldBe(createTransferResponse);
        }

        [Fact]
        private async Task ShouldRetrieveEntityBalances()
        {
            var request = new BalancesQuery();
            var responseAsync = new BalancesResponse();

            _balancesClient.Setup(apiClient =>
                    apiClient.Query<BalancesResponse>("balances/entity_id", It.IsAny<SdkAuthorization>(), request,
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => responseAsync);

            var response = await _marketplaceClient.RetrieveEntityBalances("entity_id", request);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdatePayoutSchedule()
        {
            var responseAsync = new VoidResponse();

            _apiClient
                .Setup(x =>
                    x.Put<VoidResponse>(
                        "marketplace/entities/entity_id/payout-schedules",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        null
                    )
                )
                .ReturnsAsync(responseAsync);

            var response =
                await _marketplaceClient.UpdatePayoutSchedule("entity_id", Currency.AED, new UpdateScheduleRequest());

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetrievePayoutSchedule()
        {
            var responseAsync = new GetScheduleResponse();

            _apiClient
                .Setup(x =>
                    x.Get<GetScheduleResponse>(
                        "marketplace/entities/entity_id/payout-schedules",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseAsync);

            var response = await _marketplaceClient.RetrievePayoutSchedule(
                "entity_id");

            response.ShouldNotBeNull();
        }
    }
}