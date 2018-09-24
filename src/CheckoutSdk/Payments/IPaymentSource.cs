namespace Checkout.Payments
{
    public interface IPaymentSource
    {
        /// <summary>
        /// The payment source type. For any payment request sources that result in a card token (token, source ID etc.) this will be card otherwise the name of the alternative payment method.
        /// </summary>
        string Type { get; }
    }
}