namespace Checkout.Issuing.Cards.Requests.Create
{
    public class CardVirtualRequest : CardRequest
    {
        public CardVirtualRequest() : base(CardType.Virtual)
        {
        }

        public bool IsSingleUse { get; set; }
    }
}