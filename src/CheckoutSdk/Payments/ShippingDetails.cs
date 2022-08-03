using Checkout.Common;

namespace Checkout.Payments
{
    public class ShippingDetails
    {
        public Address Address { get; set; }

        public Phone Phone { get; set; }

        public string FromAddressZip { get; set; }
    }
}