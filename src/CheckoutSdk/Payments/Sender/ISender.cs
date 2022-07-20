namespace Checkout.Payments.Sender
{
    public interface ISender
    {
        PaymentSenderType? Type();
    }
}