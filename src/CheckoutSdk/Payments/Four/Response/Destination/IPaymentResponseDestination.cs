namespace Checkout.Payments.Four.Response.Destination
{
    public interface IPaymentResponseDestination
    {
        public PaymentDestinationType? Type();
    }
}