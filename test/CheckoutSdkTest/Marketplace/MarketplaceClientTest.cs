using Checkout.Common;
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
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly MarketplaceClient _marketplaceClient;

        public MarketplaceClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory, Environment.Sandbox);
            _marketplaceClient =
                new MarketplaceClient(_apiClient.Object, _apiFilesClient.Object, configuration.Object);
        }

        [Fact]
        public async Task ShouldCreateEntity() //throws ExecutionException, InterruptedException
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
        private async Task ShouldFailSubmitFile()
        {
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory, Environment.Sandbox);
            MarketplaceClient marketplaceClient =
                new MarketplaceClient(_apiClient.Object, null, configuration.Object);
            var exception = await Assert.ThrowsAsync<CheckoutFileException>(
                () => marketplaceClient.SubmitFile(new MarketplaceFileRequest
                {
                    File = "file", ContentType = null, Purpose = MarketplaceFilePurpose.Identification
                }));

            exception.Message.ShouldBe(
                "Files API is not enabled in this client. It must be enabled in CheckoutFourSdk configuration.");
        }
    }
}