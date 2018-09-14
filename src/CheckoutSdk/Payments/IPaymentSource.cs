namespace Checkout.Payments
{
    public interface IPaymentSource
    {
        string Type { get; }
    }
}