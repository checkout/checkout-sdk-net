using Checkout.Common;


namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class ShippingDetails
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