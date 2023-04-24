namespace Checkout.Issuing.Cards.Type
{
    public class CardTypePhysicalRequest : CardTypeRequest
    {
        public CardTypePhysicalRequest() : base(CardType.Physical)
        {
        }

        public IssuingShippingInstructions ShippingInstructions { get; set; }
    }
}