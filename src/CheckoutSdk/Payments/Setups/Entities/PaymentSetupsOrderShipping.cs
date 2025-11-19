using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsOrderShipping
    {
        public Address Address { get; set; }

        public string Method { get; set; }
    }
}