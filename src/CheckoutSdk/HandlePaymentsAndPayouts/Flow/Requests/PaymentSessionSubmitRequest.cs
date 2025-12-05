namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Request to submit a payment session.
    /// </summary>
    public class PaymentSessionSubmitRequest : PaymentSessionBase
    {
        /// <summary>
        /// A unique token representing the additional customer data captured by Flow, 
        /// as received from the handleSubmit callback.
        /// Do not log or store this value.
        /// [Required]
        /// </summary>
        public string SessionData { get; set; }

        /// <summary>
        /// Deprecated - The Customer's IP address. Only IPv4 and IPv6 addresses are accepted.
        /// </summary>
        public string IpAddress { get; set; }
    }
}