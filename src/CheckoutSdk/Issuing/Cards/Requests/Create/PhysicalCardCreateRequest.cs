using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public class PhysicalCardCreateRequest : AbstractCardCreateRequest
    {
        public PhysicalCardCreateRequest() : base(IssuingCardType.Physical)
        {
        }

        public ShippingInstruction ShippingInstructions { get; set; }
    }
}