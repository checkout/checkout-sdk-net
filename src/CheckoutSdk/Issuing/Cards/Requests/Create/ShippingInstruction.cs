using Checkout.Common;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public class ShippingInstruction
    {
        public string ShippingRecipient { get; set; }

        public Address ShippingAddress { get; set; }

        public string AdditionalComment { get; set; }
    }
}