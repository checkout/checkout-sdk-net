namespace Checkout.Common
{
    /// <summary>
    /// Defines the account holder details.
    /// </summary>
    public class AccountHolder
    {
        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public Address BillingAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the phone details.
        /// </summary>
        public Phone Phone { get; set; }
    }
}