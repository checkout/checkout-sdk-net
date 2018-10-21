namespace Checkout.Payments
{
    /// <summary>
    /// Defines a payment response catering for both pending and processed response scenarios.
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Gets or sets the processed response returned following a successfully processed payment (HTTP Status Code 201).
        /// </summary>
        public PaymentProcessed Payment { get; set; }

        /// <summary>
        /// Gets or sets the pending response returned for asynchronous payments or when further action such as a redirect is required (HTTP Status Code 202).
        /// </summary>
        public PaymentPending Pending { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the payment is in a pending state.
        /// </summary>
        public bool IsPending => Pending != null;

        /// <summary>
        /// Enables the implicit conversion of <see cref="PaymentPending"/> to <see cref="PaymentResponse"/>.
        /// This is required for dynamic dispatch during the deserialization of payment responses.
        /// </summary>
        /// <param name="pendingResponse">The pending response.</param>
        public static implicit operator PaymentResponse(PaymentPending pendingResponse)
        {
            return new PaymentResponse { Pending = pendingResponse };
        }

        /// <summary>
        /// Enables the implicit conversion of <see cref="PaymentProcessed"/> to <see cref="PaymentResponse"/>.
        /// This is required for dynamic dispatch during the deserialization of payment responses.
        /// </summary>
        /// <param name="processedResponse">The processed response.</param>
        public static implicit operator PaymentResponse(PaymentProcessed processedResponse)
        {
            return new PaymentResponse { Payment = processedResponse };
        }
    }
}