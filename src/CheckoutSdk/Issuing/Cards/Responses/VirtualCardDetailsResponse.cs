namespace Checkout.Issuing.Cards.Responses
{
    public class VirtualCardDetailsResponse : CardDetailsResponse
    {
        public VirtualCardDetailsResponse() : base(CardType.Virtual)
        {
        }

        public bool IsSingleUse { get; set; }
    }
}