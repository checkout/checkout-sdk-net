using Checkout.Common;
using Checkout.Payments.Previous.Request;
using Checkout.Payments.Previous.Request.Source;
using Checkout.Payments.Previous.Response;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class PaymentsClientTest : UnitTestFixture
    {
        private const string PaymentsPath = "payments";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public PaymentsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Production, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldRequestPayment()
        {
            var paymentRequest = new PaymentRequest();
            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayment(paymentRequest, null, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldRequestPayment_CustomSource()
        {
            CustomSource customSource = new CustomSource {amount = 10L, currency = Currency.USD};

            var paymentRequest = new PaymentRequest();
            paymentRequest.Source = customSource;

            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayment(paymentRequest, null, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }


        private class CustomSource : AbstractRequestSource
        {
            public CustomSource() : base(PaymentSourceType.Alipay)
            {
            }

            public long? amount { get; set; }

            public Currency? currency { get; set; }
        }

        [Fact]
        private async Task ShouldRequestPayment_IdempotencyKey()
        {
            var paymentRequest = new PaymentRequest();
            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, paymentRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayment(paymentRequest, "test", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldRequestPayout()
        {
            var payoutRequest = new PayoutRequest();
            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, payoutRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayout(payoutRequest, null, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldRequestPayout_IdempotencyKey()
        {
            var payoutRequest = new PayoutRequest();
            var paymentResponse = new PaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentResponse>(PaymentsPath, _authorization, payoutRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RequestPayout(payoutRequest, "test", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldGetPaymentsList()
        {
            var query = new PaymentsQueryFilter();
            var responsePayments = new PaymentsQueryResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<PaymentsQueryResponse>(PaymentsPath, _authorization, query, CancellationToken.None))
                .ReturnsAsync(() => responsePayments);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.GetPaymentsList(query, CancellationToken.None);
            
            response.ShouldNotBeNull();
            response.ShouldBeSameAs(responsePayments);
        }

        [Fact]
        private async Task ShouldGetPaymentDetails()
        {
            var paymentResponse = new GetPaymentResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetPaymentResponse>(PaymentsPath + "/payment_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => paymentResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.GetPaymentDetails("payment_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentResponse);
        }

        [Fact]
        private async Task ShouldGetPaymentActions()
        {
            var paymentActions = new ItemsResponse<PaymentAction>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<PaymentAction>>(PaymentsPath + "/payment_id/actions", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => paymentActions);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.GetPaymentActions("payment_id", CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentActions);
        }

        [Fact]
        private async Task ShouldCapturePayment_Id()
        {
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>(PaymentsPath + "/payment_id/captures", _authorization,
                        null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => captureResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.CapturePayment("payment_id", null);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCapturePayment_IdempotencyKey()
        {
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>(PaymentsPath + "/payment_id/captures", _authorization,
                        null,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => captureResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.CapturePayment("payment_id", null, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCapturePayment_Request()
        {
            var captureRequest = new CaptureRequest();
            var captureResponse = new CaptureResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CaptureResponse>(PaymentsPath + "/payment_id/captures", _authorization,
                        captureRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => captureResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response =
                await paymentsClient.CapturePayment("payment_id", captureRequest, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRefundPayment_Id()
        {
            var refundResponse = new RefundResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RefundResponse>(PaymentsPath + "/payment_id/refunds", _authorization,
                        null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => refundResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RefundPayment("payment_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRefundPayment_IdempotencyKey()
        {
            var refundResponse = new RefundResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RefundResponse>(PaymentsPath + "/payment_id/refunds", _authorization,
                        null,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => refundResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.RefundPayment("payment_id", null, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRefundPayment_Request()
        {
            var refundRequest = new RefundRequest();
            var refundResponse = new RefundResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<RefundResponse>(PaymentsPath + "/payment_id/refunds", _authorization,
                        refundRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => refundResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response =
                await paymentsClient.RefundPayment("payment_id", refundRequest, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment_Id()
        {
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>(PaymentsPath + "/payment_id/voids", _authorization,
                        null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => voidResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.VoidPayment("payment_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment_IdempotencyKey()
        {
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>(PaymentsPath + "/payment_id/voids", _authorization,
                        null,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => voidResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.VoidPayment("payment_id", null, "test");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidPayment_Request()
        {
            var voidRequest = new VoidRequest();
            var voidResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<VoidResponse>(PaymentsPath + "/payment_id/voids", _authorization,
                        voidRequest,
                        CancellationToken.None, "test"))
                .ReturnsAsync(() => voidResponse);

            IPaymentsClient paymentsClient = new PaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await paymentsClient.VoidPayment("payment_id", voidRequest, "test");

            response.ShouldNotBeNull();
        }
    }
}