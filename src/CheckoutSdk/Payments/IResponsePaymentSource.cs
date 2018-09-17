namespace Checkout.Payments
{
    public interface IResponsePaymentSource
    {
        string Id { get; }
        string Type { get; }
    }
}
