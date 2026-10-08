namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Details of the customer's email.
    /// </summary>
    public class CustomerEmail
    {
        /// <summary>
        /// The customer's email address.
        /// [Optional]
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Specifies whether the customer's email address is verified.
        /// [Optional]
        /// </summary>
        public bool? Verified { get; set; }
    }
}
