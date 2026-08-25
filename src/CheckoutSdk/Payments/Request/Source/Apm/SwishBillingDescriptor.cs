namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The payment billing descriptor for a Swish payment.
    /// This deliberately does not reuse Checkout.Payments.BillingDescriptor, which adds a city and a
    /// reference that the Swish source does not declare.
    /// </summary>
    public class SwishBillingDescriptor
    {
        /// <summary>
        /// The billing descriptor name.
        /// [Required]
        /// max 120 characters
        /// </summary>
        public string Name { get; set; }
    }
}
