namespace Checkout.Payments.Response.Destination
{
    public abstract class AbstractPaymentResponseDestination
    {
        public PaymentDestinationType? Type { get; set; }
    }
}