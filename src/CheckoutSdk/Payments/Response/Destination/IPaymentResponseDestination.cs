namespace Checkout.Payments.Response.Destination
{
    public interface IPaymentResponseDestination
    {
        public PaymentDestinationType? Type();
    }
}