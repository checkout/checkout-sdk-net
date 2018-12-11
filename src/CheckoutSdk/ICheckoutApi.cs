using Checkout.Payments;
using Checkout.Sources;
using Checkout.Tokens;

namespace Checkout
{
    /// <summary>
    /// Convenience interface that provides access to the available Checkout.com APIs.
    /// </summary>
    public interface ICheckoutApi
    {
        /// <summary>
        /// Gets the Payments API.
        /// </summary>
        IPaymentsClient Payments { get; }

        /// <summary>
        /// Gets the Sources API.
        /// </summary>
        ISourcesClient Sources { get; }

        /// <summary>
        /// Gets the Tokenization API.
        /// </summary>
        ITokensClient Tokens { get; }
    }
}