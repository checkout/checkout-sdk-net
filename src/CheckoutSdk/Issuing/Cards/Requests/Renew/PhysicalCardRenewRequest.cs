using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Renew
{
    public class PhysicalCardRenewRequest : AbstractRenewCardRequest
    {
        public PhysicalCardRenewRequest() : base(IssuingCardType.Physical)
        {
        }

        public ShippingInstruction ShippingInstructions { get; set; }
    }
}