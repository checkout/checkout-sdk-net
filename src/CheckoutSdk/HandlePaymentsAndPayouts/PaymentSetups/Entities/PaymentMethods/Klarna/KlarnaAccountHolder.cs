using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class KlarnaAccountHolder
    {
        /// <summary>
        /// The billing address of the Klarna account holder
        /// </summary>
        public Address BillingAddress { get; set; }
    }
}