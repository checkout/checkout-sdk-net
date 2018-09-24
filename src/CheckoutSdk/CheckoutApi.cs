using Checkout.Payments;
using Checkout.Tokens;

namespace Checkout
{
    public class CheckoutApi : ICheckoutApi
    {
        public CheckoutApi(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            Payments = new PaymentsClient(apiClient, configuration);
            Tokens = new TokensClient(apiClient, configuration);
        }

        public IPaymentsClient Payments { get; }
        public ITokensClient Tokens { get; }


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