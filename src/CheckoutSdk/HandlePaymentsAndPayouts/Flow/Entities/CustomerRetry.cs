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
    }
}
