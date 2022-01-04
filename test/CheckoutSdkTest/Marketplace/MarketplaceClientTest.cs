using Checkout.Common;
using Checkout.Files;
using Moq;
using Shouldly;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Marketplace.Test
{
    public class MarketplaceClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<ICheckoutConfiguration> _configuration;

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<IFilesClient> _fileClient = new Mock<IFilesClient>();
        private readonly MarketplaceClient _marketplaceClient;

        public MarketplaceClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);
            _configuration = new Mock<ICheckoutConfiguration>();
            _configuration.Setup(s => s.SdkCredentials).Returns(_sdkCredentials.Object);
            _configuration.Setup(s => s.Environment).Returns(Environment.Sandbox);
            _configuration.Setup(s => s.HttpClientFactory).Returns(_httpClientFactory.Object);

            _marketplaceClient = new MarketplaceClient(_apiClient.Object, _configuration.Object, _fileClient.Object);
        }

        [Fact]
        public async Task ShouldCreateEntity() //throws ExecutionException, InterruptedException
        {
            var onboardEntityResponse = new OnboardEntityResponse()
            {
                Id = "Id"
            };

            _apiClient.Setup(x => x.Post<OnboardEntityResponse>(BuildPath(MarketplaceClient.MarketplacePath, MarketplaceClient.EntitiesPath), It.IsAny<SdkAuthorization>(), It.IsAny<object>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(onboardEntityResponse);

            var response = await _marketplaceClient.CreateEntity(new OnboardEntityRequest());

            response.ShouldNotBeNull();
            response.ShouldBe(onboardEntityResponse);
        }

        [Fact]
        private async void ShouldGetEntity()
        {
            var responseObject = new OnboardEntityDetailsResponse()
            {
                Id = "entity_id"
            };

            _apiClient
                .Setup(x =>
                    x.Get<OnboardEntityDetailsResponse>(
                        BuildPath(MarketplaceClient.MarketplacePath, MarketplaceClient.EntitiesPath, responseObject.Id),
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
            var responseObject = new OnboardEntityResponse()
            {
                Id = "entity_id",
                Reference = "A"
            };

            _apiClient
                .Setup(x =>
                    x.Put<OnboardEntityResponse>(
                        BuildPath(MarketplaceClient.MarketplacePath, MarketplaceClient.EntitiesPath, responseObject.Id),
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()))
                .ReturnsAsync(responseObject);

            var response = await _marketplaceClient.UpdateEntity(
                new OnboardEntityRequest()
                {
                    Reference = "A"
                },
                responseObject.Id);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(responseObject.Id);
            response.Reference.ShouldBe(responseObject.Reference);
        }

        [Fact]
        private async Task ShouldCreatePaymentInstrument()
        {
            var responseObject = new OnboardEntityDetailsResponse()
            {
                Id = "entity_id",
            };

            _apiClient
                .Setup(x =>
                    x.Post<OnboardEntityDetailsResponse>(
                        BuildPath(MarketplaceClient.MarketplacePath, MarketplaceClient.EntitiesPath, responseObject.Id, MarketplaceClient.InstrumentPath),
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<object>(),
                        It.IsAny<CancellationToken>(),
                        It.IsAny<string>()));

            await _marketplaceClient.CreatePaymentInstrument(
                new MarketplacePaymentInstrument()
                {
                    AccountNumber = "324445"
                },
                responseObject.Id);
        }

        [Fact]
        private async Task ShouldSubmitFile()
        {
            //Arrange
            var responseObject = new IdResponse
            {
                Id = "Id"
            };

            var workingDirectory = System.Environment.CurrentDirectory;

            int index = workingDirectory.IndexOf("CheckoutSdkTest") + 15;

            var path = workingDirectory.Remove(index, workingDirectory.Count() - index);

            FileStream file;

            if (File.Exists(path + "\\marketplace\\testfile.png"))
            {
                file = File.OpenRead(path + "\\marketplace\\testfile.png");
                path = file.Name;
                file.Close();
            }
            else if (File.Exists(path + "/marketplace/testfile.png"))
            {
                file = File.OpenRead(path + "/marketplace/testfile.png");
                path = file.Name;
                file.Close();
            }

            path.ShouldNotBeEmpty();

            _configuration
                .Setup(x => x.GetFilesApiConfiguration())
                .Returns(new CheckoutFilesConfiguration(Environment.Sandbox));

            _apiClient
                .Setup(x =>
                    x.PostFile<IdResponse>(
                        It.IsAny<string>(),
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<MultipartFormDataContent>(),
                        It.IsAny<CancellationToken>(),
                        null
                        )
                    )
                .ReturnsAsync(responseObject);

            var response = await _marketplaceClient.SubmitFile(new MarketplaceFileRequest(path, null, MarketplaceFilePurpose.Identification));

            response.ShouldNotBeNull();
            response.ShouldBe(responseObject);
        }

        [Fact]
        private async Task ShouldFailSubmitFile()
        {
            _configuration
               .Setup(x => x.GetFilesApiConfiguration())
               .Returns(new CheckoutFilesConfiguration());

            var exception = await Assert.ThrowsAsync<CheckoutFileException>(
                   () => _marketplaceClient.SubmitFile(new MarketplaceFileRequest("file", null, MarketplaceFilePurpose.Identification))
               );

            exception.Message.ShouldBe("Files API is not enabled in this client. It must be enabled in CheckoutFourSdk configuration.");
        }
    }
}