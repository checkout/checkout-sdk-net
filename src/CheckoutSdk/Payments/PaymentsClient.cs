using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Payments.Request;
using Checkout.Payments.Response;

namespace Checkout.Payments
{
    public class PaymentsClient : AbstractClient, IPaymentsClient
    {
        private const string PaymentsPath = "payments";

        public PaymentsClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<PaymentResponse> RequestPayment(PaymentRequest paymentRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentRequest", paymentRequest);
            return ApiClient.Post<PaymentResponse>(
                PaymentsPath,
                SdkAuthorization(),
                paymentRequest,
                cancellationToken,
                idempotencyKey);
        }

        public Task<PaymentResponse> RequestPayout(PayoutRequest payoutRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("payoutRequest", payoutRequest);
            return ApiClient.Post<PaymentResponse>(
                PaymentsPath,
                SdkAuthorization(),
                payoutRequest,
                cancellationToken,
                idempotencyKey);
        }

        public Task<GetPaymentResponse> GetPaymentDetails(
            string paymentId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Get<GetPaymentResponse>(BuildPath(PaymentsPath, paymentId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<IList<PaymentAction>> GetPaymentActions(
            string paymentId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Get<IList<PaymentAction>>(BuildPath(PaymentsPath, paymentId, "actions"),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<CaptureResponse> CapturePayment(
            string paymentId,
            CaptureRequest captureRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Post<CaptureResponse>(BuildPath(PaymentsPath, paymentId, "captures"),
                SdkAuthorization(),
                captureRequest,
                cancellationToken,
                idempotencyKey);
        }

        public Task<RefundResponse> RefundPayment(
            string paymentId,
            RefundRequest refundRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Post<RefundResponse>(BuildPath(PaymentsPath, paymentId, "refunds"),
                SdkAuthorization(),
                refundRequest,
                cancellationToken,
                idempotencyKey);
        }

        public Task<VoidResponse> VoidPayment(
            string paymentId,
            VoidRequest voidRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId);
            return ApiClient.Post<VoidResponse>(BuildPath(PaymentsPath, paymentId, "voids"),
                SdkAuthorization(),
                voidRequest,
                cancellationToken,
                idempotencyKey);
        }
    }
}