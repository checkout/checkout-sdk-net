namespace Checkout.Payments.Response.Destination
{
    public interface IPaymentResponseDestination
    {
        PaymentDestinationType? Type();
    }
}