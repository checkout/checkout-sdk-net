using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        /// <summary>
        /// Request a payment
        /// </summary>
        /// <typeparam name="TPaymentSource">The source of the payment</typeparam>
        /// <param name="paymentRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaymentResponse> RequestAsync<TPaymentSource>(PaymentRequest<TPaymentSource> paymentRequest, CancellationToken cancellationToken = default(CancellationToken)) 
            where TPaymentSource : IPaymentSource;
        /// <summary>
        /// Returns the details of the payment with the specified identifier string.
        /// If the payment method requires a redirection to a third party(e.g. 3D-Secure), the redirect URL back to your site will include a cko-session-id query parameter containing a payment session ID that can be used to obtain the details of the payment, for example:
        /// http://example.com/success?cko-session-id=sid_ubfj2q76miwundwlk72vxt2i7q
        /// </summary>
        /// <param name="paymentId">The payment or payment session identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetPaymentDetailsResponse> GetAsync(string paymentId, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Returns all the actions associated with a payment ordered by processing date in descending order (latest first).
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<PaymentAction>> GetActionsAsync(string paymentId, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Captures a payment if supported by the payment method.
        /// For card payments, capture requests are processed asynchronously.You can use webhooks to be notified if the capture is successful.
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="captureRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CaptureResponse> CaptureAsync(string paymentId, CaptureRequest captureRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Refunds a payment if supported by the payment method.
        /// For card payments, refund requests are processed asynchronously.You can use webhooks to be notified if the refund is successful.
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="refundRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RefundResponse> RefundAsync(string paymentId, RefundRequest refundRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Voids a payment if supported by the payment method.
        /// For card payments, void requests are processed asynchronously.You can use webhooks to be notified if the void is successful.
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="voidRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VoidResponse> VoidAsync(string paymentId, VoidRequest voidRequest = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}