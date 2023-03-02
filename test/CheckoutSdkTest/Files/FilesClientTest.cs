using Checkout.Common;
using Moq;
using Shouldly;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Files
{
    public class FilesClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public FilesClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldSubmitFile()
        {
            const string filePath = "./Resources/checkout.jpeg";
            var idResponse = new IdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdResponse>(
                        "files",
                        _authorization,
                        It.IsAny<MultipartFormDataContent>(),
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => idResponse);

            IFilesClient client = new FilesClient(_apiClient.Object, null, _configuration.Object);

            var response = await client.SubmitFile(filePath, "dispute_evidence");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetFileDetails()
        {
            const string fileId = "file_1351861";
            var idResponse = new FileDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<FileDetailsResponse>($"files/{fileId}", _authorization, CancellationToken.None))
                .ReturnsAsync(() => idResponse);

            IFilesClient client = new FilesClient(_apiClient.Object, null, _configuration.Object);

            var response = await client.GetFileDetails(fileId);

            response.ShouldNotBeNull();
        }
    }
}