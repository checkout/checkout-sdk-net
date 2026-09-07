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
                Environment.Sandbox, _httpClientFactory);
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

        [Fact]
        private async Task ShouldRetrieveTopUpInstructions()
        {
            var responseAsync = new TopUpInstructionsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<TopUpInstructionsResponse>(
                        "entities/ent_w4jelhppmfiufdnatam37wrfc4/currency-accounts/ca_g5y7d6jo4e2urgforcbf2ey5jm/top-up-instructions",
                        It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => responseAsync);

            var response = await _balancesClient.RetrieveTopUpInstructions("ent_w4jelhppmfiufdnatam37wrfc4",
                "ca_g5y7d6jo4e2urgforcbf2ey5jm");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(responseAsync);
        }

        [Theory]
        [InlineData(null, "ca_g5y7d6jo4e2urgforcbf2ey5jm")]
        [InlineData("", "ca_g5y7d6jo4e2urgforcbf2ey5jm")]
        [InlineData("ent_w4jelhppmfiufdnatam37wrfc4", null)]
        [InlineData("ent_w4jelhppmfiufdnatam37wrfc4", "")]
        private async Task ShouldThrowWhenTopUpInstructionsIdentifiersAreMissing(string entityId,
            string currencyAccountId)
        {
            await Should.ThrowAsync<CheckoutArgumentException>(() =>
                _balancesClient.RetrieveTopUpInstructions(entityId, currencyAccountId));
        }
    }
}
