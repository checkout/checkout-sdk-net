using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Financial
{
    public class FinancialClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly FinancialClient _financialClient;

        public FinancialClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);
            Mock<CheckoutConfiguration> configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory, null);
            _financialClient =
                new FinancialClient(_apiClient.Object, configuration.Object);
        }

        [Fact]
        private async Task ShouldQueryFinancialActions()
        {
            var query = new FinancialActionsQueryFilter();
            var responseAsync = new FinancialActionsQueryResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<FinancialActionsQueryResponse>(
                        "financial-actions",
                        It.IsAny<SdkAuthorization>(),
                        query,
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => responseAsync);

            var response = await _financialClient.Query(query);

            response.ShouldNotBeNull();
        }
    }
}
