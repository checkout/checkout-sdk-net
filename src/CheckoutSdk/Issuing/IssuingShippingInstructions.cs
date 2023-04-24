using Checkout.Common;

namespace Checkout.Issuing
{
    public class IssuingShippingInstructions
    {
        public string ShippingRecipient { get; set; }

        public Address ShippingAddress { get; set; }

        public string AdditionalComment { get; set; }
    }
}