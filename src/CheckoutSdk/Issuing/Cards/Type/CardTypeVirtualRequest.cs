namespace Checkout.Issuing.Cards.Type
{
    public class CardTypeVirtualRequest : CardTypeRequest
    {
        public CardTypeVirtualRequest() : base(CardType.Virtual)
        {
        }

        public bool IsSingleUse { get; set; }
    }
}