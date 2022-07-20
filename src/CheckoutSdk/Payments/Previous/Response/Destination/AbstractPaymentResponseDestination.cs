namespace Checkout.Payments.Previous.Response.Destination
{
    public abstract class AbstractPaymentResponseDestination
    {
        public PaymentDestinationType? Type { get; set; }
    }
}