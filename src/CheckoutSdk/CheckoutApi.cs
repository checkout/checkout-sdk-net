using Checkout.Payments;
using Checkout.Sources;
using Checkout.Tokens;
using Checkout.Disputes;
using Checkout.Files;
using Checkout.Webhooks;

namespace Checkout
{
    /// <summary>
    /// Default implementation of <see cref="ICheckoutApi"/> that defines the available Checkout.com APIs.
    /// </summary>
    public class CheckoutApi : ICheckoutApi
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutApi"/> instance and initializes each underlying API client.
        /// </summary>
        /// <param name="apiClient">The API client used to send API requests and handle responses.</param>
        /// <param name="configuration">A configuration object containing authentication and API specific information.</param>
        public CheckoutApi(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            Payments = new PaymentsClient(apiClient, configuration);
            Sources = new SourcesClient(apiClient, configuration);
            Tokens = new TokensClient(apiClient, configuration);
            Disputes = new DisputesClient(apiClient, configuration);
            Files = new FilesClient(apiClient, configuration);
            Webhooks = new WebhooksClient(apiClient, configuration);
        }

        /// <summary>
        /// Gets the Payments API.
        /// </summary>
        public IPaymentsClient Payments { get; }

        /// <summary>
        /// Gets the Sources API.
        /// </summary>
        public ISourcesClient Sources { get; }

        /// <summary>
        /// Gets the Tokenization API. 
        /// </summary>
        public ITokensClient Tokens { get; }

        /// <summary>
        /// Gets the Disutes API. 
        /// </summary>
        public IDisputesClient Disputes { get; }

        /// <summary>
        /// Gets the Files API. 
        /// </summary>
        public IFilesClient Files { get; }

        /// <summary>
        /// Gets the Webhooks API. 
        /// </summary>
        public IWebhooksClient Webhooks { get; }

        /// <summary>
        /// Creates a new <see cref="CheckoutApi"/> instance with default dependencies.
        /// </summary>
        /// <param name="secretKey">Your secret key obtained from the Checkout Hub.</param>
        /// <param name="useSandbox">Whether to connect to the Checkout Sandbox. False indicates the live environment should be used.</param>
        /// <param name="publicKey">Your public key obtained from the Checkout Hub. Required for some API resources.</param>
        /// <returns>The configured instance.</returns>
        public static CheckoutApi Create(string secretKey, bool useSandbox = true, string publicKey = null)
        {
            var configuration = new CheckoutConfiguration(secretKey, useSandbox)
            {
                PublicKey = publicKey
            };

            var apiClient = new ApiClient(configuration);
            return new CheckoutApi(apiClient, configuration);
        }

        /// <summary>
        /// Creates a new <see cref="CheckoutApi"/> instance with default dependencies.
        /// </summary>
        /// <param name="secretKey">Your secret key obtained from the Checkout Hub.</param>
        /// <param name="uri">The base URL of the Checkout API you wish to connect to.</param>
        /// <param name="publicKey">Your public key obtained from the Checkout Hub. Required for some API resources.</param>
        /// <returns>The configured instance.</returns>
        public static CheckoutApi Create(string secretKey, string uri, string publicKey = null)
        {
            var configuration = new CheckoutConfiguration(secretKey, uri)
            {
                PublicKey = publicKey
            };

            var apiClient = new ApiClient(configuration);
            return new CheckoutApi(apiClient, configuration);
        }
    }
}
