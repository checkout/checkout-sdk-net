namespace Checkout.Payments
{
    /// <summary>
    /// Defines a payment source specified in a payment request.
    /// </summary>
    public interface IRequestSource
    {
        /// <summary>
        /// Gets the payment source type. 
        /// </summary>
        string Type { get; }
    }
}