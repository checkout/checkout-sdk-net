namespace Checkout.Issuing.Cards.Responses
{
    public class PhysicalCardDetailsResponse : CardDetailsResponse
    {
        public PhysicalCardDetailsResponse() : base(CardType.Physical)
        {
        }
    }
}