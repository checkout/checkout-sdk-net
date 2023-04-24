namespace Checkout.Issuing.Cards.Requests.Create
{
    public class CardPhysicalRequest : CardRequest
    {
        public CardPhysicalRequest() : base(CardType.Physical)
        {
        }

        public ShippingInstruction ShippingInstructions { get; set; }
    }
}