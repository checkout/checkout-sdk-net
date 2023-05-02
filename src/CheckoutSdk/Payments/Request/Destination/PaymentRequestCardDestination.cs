namespace Checkout.Payments.Request.Destination
{
    public class PaymentRequestCardDestination : PaymentRequestDestination
    {
        public PaymentRequestCardDestination() : base(PaymentDestinationType.Card)
        {
        }

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }
    }
}