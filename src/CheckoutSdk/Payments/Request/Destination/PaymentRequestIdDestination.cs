namespace Checkout.Payments.Request.Destination
{
    public sealed class PaymentRequestIdDestination : PaymentRequestDestination
    {
        public PaymentRequestIdDestination() : base(PaymentDestinationType.Id)
        {
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}