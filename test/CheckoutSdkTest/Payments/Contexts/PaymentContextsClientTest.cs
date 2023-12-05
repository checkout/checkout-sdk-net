using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Contexts
{

    public class PaymentContextsClientTest : UnitTestFixture
    {
        private const string PaymentContexts = "payment-contexts";

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<CheckoutConfiguration> _configuration;

        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);

        public PaymentContextsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestAPaymentContext()
        {
            PaymentContextsRequest paymentContextsRequest = new PaymentContextsRequest();
            PaymentContextsRequestResponse paymentContextsRequestResponse = new PaymentContextsRequestResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentContextsRequestResponse>(PaymentContexts, _authorization,
                        paymentContextsRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => paymentContextsRequestResponse);

            PaymentContextsClient client = new PaymentContextsClient(_apiClient.Object, _configuration.Object);
            PaymentContextsRequestResponse response =
                await client.RequestPaymentContexts(paymentContextsRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetAPaymentContext()
        {
            PaymentContextDetailsResponse paymentContextsDetailsResponse = new PaymentContextDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<PaymentContextDetailsResponse>(
                        PaymentContexts + "/id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => paymentContextsDetailsResponse);

            PaymentContextsClient client = new PaymentContextsClient(_apiClient.Object, _configuration.Object);

            PaymentContextDetailsResponse
                response = await client.GetPaymentContextDetails("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}