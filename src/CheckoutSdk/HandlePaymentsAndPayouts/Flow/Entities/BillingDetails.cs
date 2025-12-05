using Checkout.Common;


namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class BillingDetails
    {
        /// <summary>
        /// The billing address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The phone number associated with the billing address
        /// </summary>
        public Phone Phone { get; set; }
    }
}