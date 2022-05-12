namespace Checkout.Payments.Four.Sender
{
    public interface ISender
    {
        PaymentSenderType? Type();
    }
}