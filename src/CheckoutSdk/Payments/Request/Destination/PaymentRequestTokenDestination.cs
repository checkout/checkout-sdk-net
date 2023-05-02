namespace Checkout.Payments.Request.Destination
{
    public class PaymentRequestTokenDestination : PaymentRequestDestination
    {
        public PaymentRequestTokenDestination() : base(PaymentDestinationType.Token)
        {
        }

        public string Token { get; set; }
    }
}