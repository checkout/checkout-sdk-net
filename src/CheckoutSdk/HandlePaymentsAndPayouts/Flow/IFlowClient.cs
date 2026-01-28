using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.HandlePaymentsAndPayouts.Flow.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    /// <summary>
    /// Flow - Create payment sessions and submit payment attempts
    /// </summary>
    public interface IFlowClient
    {
        /// <summary>
        /// Request a Payment Session
        /// Creates a payment session.
        /// The values you provide in the request will be used to determine the payment methods available to Flow. 
        /// Some payment methods may require you to provide specific values for certain fields.
        /// You must supply the unmodified response body when you initialize Flow.
        /// </summary>
        Task<PaymentSessionResponse> RequestPaymentSession(PaymentSessionCreateRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Submit a Payment Session
        /// Submit a payment attempt for a payment session.
        /// This request works with the Flow handleSubmit callback, where you can perform a customized payment submission.
        /// You must send the unmodified response body as the response of the handleSubmit callback.
        /// </summary>
        Task<PaymentSubmissionResponse> SubmitPaymentSession(string sessionId, PaymentSessionSubmitRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Request a Payment Session with Payment
        /// Create a payment session and submit a payment attempt for it.
        /// The values you provide in the request will be used to determine the payment methods available to Flow.
        /// This request works with the advanced Flow integration, where you do not need to create a payment session for initializing Flow.
        /// You must send the unmodified response body as the response of the handleSubmit callback.
        /// </summary>
        Task<PaymentSubmissionResponse> RequestPaymentSessionWithPayment(PaymentSessionCompleteRequest request,
            CancellationToken cancellationToken = default);
    }
}