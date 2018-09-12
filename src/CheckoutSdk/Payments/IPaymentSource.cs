namespace Checkout.Sdk.Payments
{
    public interface IPaymentSource
    {
        string Type { get; }
    }
}