using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The billing details for the payment.
    /// </summary>
    public class Billing
    {
        /// <summary>
        /// The billing address.
        /// [Optional]
        /// </summary>
        public Address Address { get; set; }
    }
}
