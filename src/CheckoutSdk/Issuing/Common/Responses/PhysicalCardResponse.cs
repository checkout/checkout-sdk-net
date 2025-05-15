namespace Checkout.Issuing.Common.Responses
{
    public class PhysicalCardResponse : AbstractCardResponse
    {
        public PhysicalCardResponse() : base(IssuingCardType.Physical)
        {
        }
    }
}