using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Apm.Bacs
{
    public class BacsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public BacsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldSendNotification()
        {
            var request = new BacsNotificationRequest();
            var expectedResponse = new BacsNotificationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<BacsNotificationResponse>("apms/bacs/notifications", _authorization,
                        request,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IBacsClient bacsClient = new BacsClient(_apiClient.Object, _configuration.Object);

            var response = await bacsClient.SendNotification(request, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }
    }
}
