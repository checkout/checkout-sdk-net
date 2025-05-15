using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Sessions
{

    public class PaymentSessionsClientTest : UnitTestFixture
    {
        private const string PaymentSessions = "payment-sessions";

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<CheckoutConfiguration> _configuration;

        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);

        public PaymentSessionsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestAPaymentSessions()
        {
            PaymentSessionsRequest paymentSessionsRequest = new PaymentSessionsRequest();
            PaymentSessionsResponse paymentSessionsResponse = new PaymentSessionsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentSessionsResponse>(PaymentSessions, _authorization,
                        paymentSessionsRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => paymentSessionsResponse);

            PaymentSessionsClient client = new PaymentSessionsClient(_apiClient.Object, _configuration.Object);
            PaymentSessionsResponse response =
                await client.RequestPaymentSessions(paymentSessionsRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}