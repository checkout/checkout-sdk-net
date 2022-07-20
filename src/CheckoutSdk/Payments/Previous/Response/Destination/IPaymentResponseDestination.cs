namespace Checkout.Payments.Previous.Response.Destination
{
    public interface IPaymentResponseDestination
    {
        PaymentDestinationType? Type();
    }
}