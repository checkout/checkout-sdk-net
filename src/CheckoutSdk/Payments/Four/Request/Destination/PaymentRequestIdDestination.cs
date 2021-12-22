namespace Checkout.Payments.Four.Request.Destination
{
    public sealed class PaymentRequestIdDestination : PaymentRequestDestination
    {
        public PaymentRequestIdDestination() : base(PaymentDestinationType.Id)
        {
        }

        public string Id { get; set; }
    }
}