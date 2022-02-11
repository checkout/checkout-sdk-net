using Checkout.Payments.Four.Request;
using Checkout.Payments.Four.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Four
{
    public interface IPaymentsClient
    {
        Task<PaymentResponse> RequestPayment(
            PaymentRequest paymentRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<PayoutResponse> RequestPayout(
            PayoutRequest payoutRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<GetPaymentResponse> GetPaymentDetails(
            string paymentId,
            CancellationToken cancellationToken = default);

        Task<IList<PaymentAction>> GetPaymentActions(
            string paymentId,
            CancellationToken cancellationToken = default);

        Task<CaptureResponse> CapturePayment(
            string paymentId,
            CaptureRequest captureRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<RefundResponse> RefundPayment(
            string paymentId,
            RefundRequest refundRequest = null,
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
    }
}