using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Balances
{
    public class BalancesClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidPreviousSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly BalancesClient _balancesClient;

        public BalancesClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory, null);
            _balancesClient =
                new BalancesClient(_apiClient.Object, configuration.Object);
        }

        [Fact]
        private async Task ShouldRetrieveEntityBalances()
        {
            var request = new BalancesQuery();
            var responseAsync = new BalancesResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<BalancesResponse>("balances/entity_id", It.IsAny<SdkAuthorization>(), request,
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => responseAsync);

            var response = await _balancesClient.RetrieveEntityBalances("entity_id", request);

            response.ShouldNotBeNull();
        }
    }
}