using Checkout.Common;

namespace Checkout.Payments
{
    public class Shipping
    {
        /// <summary>
        /// The shipping address
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// The phone number associated with the shipping address
        /// </summary>
        public Phone Phone { get; set; }
    }
}