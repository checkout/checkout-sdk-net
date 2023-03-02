using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Apm.Ideal
{
    public class IdealClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousPk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public IdealClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldGetInfo()
        {
            var idealInfo = new IdealInfo();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdealInfo>("ideal-external", _authorization, CancellationToken.None))
                .ReturnsAsync(() => idealInfo);

            var idealClient = new IdealClient(_apiClient.Object, _configuration.Object);

            var response = await idealClient.GetInfo(CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetIssuers()
        {
            var issuerResponse = new IssuerResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<IssuerResponse>("ideal-external/issuers", _authorization, CancellationToken.None))
                .ReturnsAsync(() => issuerResponse);

            var idealClient = new IdealClient(_apiClient.Object, _configuration.Object);

            var response = await idealClient.GetIssuers(CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}