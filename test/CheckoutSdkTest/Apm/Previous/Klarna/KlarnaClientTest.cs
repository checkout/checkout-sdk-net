using Checkout.Payments;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Apm.Previous.Klarna
{
    public class KlarnaClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousPk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public KlarnaClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.PublicKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldCreateCreditSession()
        {
            var creditSessionRequest = new CreditSessionRequest();
            var creditSessionResponse = new CreditSessionResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CreditSessionResponse>("klarna-external/credit-sessions", _authorization,
                        creditSessionRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => creditSessionResponse);

            var klarnaClient = new KlarnaClient(_apiClient.Object, _configuration.Object);

            var response = await klarnaClient.CreateCreditSession(creditSessionRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetCreditSession()
        {
            var creditSession = new CreditSession();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<CreditSession>("klarna-external/credit-sessions/id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => creditSession);

            var klarnaClient = new KlarnaClient(_apiClient.Object, _configuration.Object);

            var response = await klarnaClient.GetCreditSession("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCapturePayment()
        {
            var orderCaptureRequest = new OrderCaptureRequest();
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>("klarna-external/orders/id/captures", _authorization,
                        orderCaptureRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => captureResponse);

            var klarnaClient = new KlarnaClient(_apiClient.Object, _configuration.Object);

            var response = await klarnaClient.CapturePayment("id", orderCaptureRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment()
        {
            var voidRequest = new VoidRequest();
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>("klarna-external/orders/id/voids", _authorization,
                        voidRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => voidResponse);

            var klarnaClient = new KlarnaClient(_apiClient.Object, _configuration.Object);

            var response = await klarnaClient.VoidPayment("id", voidRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}