namespace Checkout.Payments.Sessions
{
    public class PaymentMethodConfiguration
    {
        public Applepay Applepay { get; set; }
        public Card Card { get; set; }
        public Googlepay Googlepay { get; set; }
    }
}