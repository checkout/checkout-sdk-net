using Checkout.Payments.Request;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    /// <summary>
    /// Configures the parameters for customer retries. A customer retry is a payment attempt performed by the
    /// customer on the checkout page. Used by hosted payments, payment links and payment sessions.
    /// </summary>
    public class CustomerRetry
    {
        /// <summary>
        /// The maximum number of customer retry attempts, excluding the initial payment attempt.
        /// [Optional] for hosted payments and payment links, [Required] for payment sessions.
        /// Range [ 0 .. 5 ]. Default: 5
        /// </summary>
        public int? MaxAttempts { get; set; } = 5;

#pragma warning disable CS0618
        /// <summary>
        /// Converts a <see cref="PaymentRetryRequest"/> into a <see cref="CustomerRetry"/>, copying only
        /// <see cref="PaymentRetryRequest.MaxAttempts"/>, the single field customer_retry accepts.
        /// Kept so code that assigned a PaymentRetryRequest to CustomerRetry keeps compiling.
        /// </summary>
        /// <param name="request">The deprecated retry request. Null converts to null.</param>
        [System.Obsolete("Use CustomerRetry instead. This conversion will be removed in the next major version.")]
        public static implicit operator CustomerRetry(PaymentRetryRequest request)
        {
            return request == null ? null : new CustomerRetry { MaxAttempts = request.MaxAttempts };
        }
#pragma warning restore CS0618
    }
}
