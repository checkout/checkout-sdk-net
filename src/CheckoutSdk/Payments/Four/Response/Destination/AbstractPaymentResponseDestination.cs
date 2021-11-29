namespace Checkout.Payments.Four.Response.Destination
{
    public abstract class AbstractPaymentResponseDestination
    {
        public PaymentDestinationType? Type { get; set; }
    }
}