namespace Checkout.Payments.Four.Response.Destination
{
    public interface IPaymentResponseDestination
    {
        PaymentDestinationType? Type();
    }
}