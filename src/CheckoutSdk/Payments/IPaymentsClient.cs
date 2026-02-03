using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Requests.ReverseAPaymentRequest;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Responses.ReverseAPaymentResponse;
using Checkout.Payments.Request;
using Checkout.Payments.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        Task<PaymentResponse> RequestPayment(
            PaymentRequest paymentRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);
        
        Task<RequestAPaymentOrPayoutResponse> RequestPayment(
            UnreferencedRefundRequest paymentRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<PayoutResponse> RequestPayout(
            PayoutRequest payoutRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<PaymentsQueryResponse> GetPaymentsList(
            PaymentsQueryFilter queryFilter,
            CancellationToken cancellationToken = default);

        Task<GetPaymentResponse> GetPaymentDetails(
            string paymentId,
            CancellationToken cancellationToken = default);

        Task<ItemsResponse<PaymentAction>> GetPaymentActions(
            string paymentId,
            CancellationToken cancellationToken = default);

        Task<CancelAScheduledRetryResponse> CancelAScheduledRetry(
            string paymentId,
            CancelAScheduledRetryRequest cancelAScheduledRetryRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<CaptureResponse> CapturePayment(
            string paymentId,
            CaptureRequest captureRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<RefundResponse> RefundPayment(
            string paymentId,
            RefundRequest refundRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);
        
        Task<ReverseAPaymentResponse> ReverseAPayment(
            string paymentId,
            ReverseAPaymentRequest reverseAPaymentRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<VoidResponse> VoidPayment(
            string paymentId,
            VoidRequest voidRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<AuthorizationResponse> IncrementPaymentAuthorization(
            string paymentId,
            AuthorizationRequest authorizationRequest = null,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<PaymentsQueryResponse> SearchPayments(
            PaymentsSearchRequest searchRequest,
            CancellationToken cancellationToken = default);
    }
}