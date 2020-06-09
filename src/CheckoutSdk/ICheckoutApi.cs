using Checkout.Payments;
using Checkout.Sources;
using Checkout.Tokens;
using Checkout.Disputes;
using Checkout.Files;
using Checkout.Webhooks;

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

        /// <summary>
        /// Gets the Disputes API.
        /// </summary>
        IDisputesClient Disputes { get; }

        /// <summary>
        /// Gets the Files API.
        /// </summary>
        IFilesClient Files { get; }

        /// <summary>
        /// Gets the Webhooks API.
        /// </summary>
        IWebhooksClient Webhooks { get; }
    }
}
