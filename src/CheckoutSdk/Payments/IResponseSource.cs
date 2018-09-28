namespace Checkout.Payments
{
    /// <summary>
    /// Defines a payment source returned in a payment response.
    /// </summary>
    public interface IResponseSource
    {
        /// <summary>
        /// Gets the final payment source type. For payment request sources that result in a card payment (token, source ID etc.) 
        /// this will be card otherwise the name of the alternative payment method.
        /// </summary>
        string Type { get; }
    }
}
