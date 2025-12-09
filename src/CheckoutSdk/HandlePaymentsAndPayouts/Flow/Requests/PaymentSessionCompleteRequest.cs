namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Request to create and complete a payment session in one operation.
    /// </summary>
    public class PaymentSessionCompleteRequest : PaymentSessionInfo
    {
        /// <summary>
        /// A unique token representing the additional customer data captured by Flow, 
        /// as received from the handleSubmit callback.
        /// Do not log or store this value.
        /// [Required]
        /// </summary>
        public string SessionData { get; set; }
    }
}