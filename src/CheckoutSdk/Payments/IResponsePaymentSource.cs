namespace Checkout.Payments
{
    public interface IResponsePaymentSource
    {
        /// <summary>
        /// The payment source identifier that can be used for subsequent payments. For new sources, this will only be returned if the payment was approved.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// The payment source type. For any payment request sources that result in a card token (token, source ID etc.) this will be card otherwise the name of the alternative payment method.
        /// </summary>
        string Type { get; }
    }
}
