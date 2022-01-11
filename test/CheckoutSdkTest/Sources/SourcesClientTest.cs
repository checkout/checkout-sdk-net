using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout.Sources
{
    public class CustomersClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CustomersClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, Environment.Sandbox);
        }


        [Fact]
        private async Task ShouldCreateSepaSource()
        {
            var sepaSourceRequest = new SepaSourceRequest();
            var sepaSourceResponse = new SepaSourceResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<SepaSourceResponse>("sources", _authorization, sepaSourceRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => sepaSourceResponse);

            ISourcesClient client = new SourcesClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreateSepaSource(sepaSourceRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}