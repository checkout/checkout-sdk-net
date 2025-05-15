namespace Checkout.Issuing.Common.Responses
{
    public class VirtualCardResponse : AbstractCardResponse
    {
        public VirtualCardResponse() : base(IssuingCardType.Virtual)
        {
        }
        
        public bool? IsSingleUse { get; set; }
    }
}