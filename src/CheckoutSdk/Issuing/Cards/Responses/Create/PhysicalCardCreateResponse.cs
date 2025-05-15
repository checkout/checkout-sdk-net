using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public class PhysicalCardCreateResponse : AbstractCardCreateResponse
    {
        public PhysicalCardCreateResponse() : base(IssuingCardType.Physical)
        {
        }
    }
}