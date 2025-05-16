using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Renew
{
    public class VirtualCardRenewRequest : AbstractRenewCardRequest
    {
        public VirtualCardRenewRequest() : base(IssuingCardType.Virtual)
        {
        }
    }
}