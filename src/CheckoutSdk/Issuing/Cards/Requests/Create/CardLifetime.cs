namespace Checkout.Issuing.Cards.Requests.Create
{
    public class CardLifetime
    {
        public LifetimeUnit Unit { get; set; }

        public int? Value { get; set; }
    }
}