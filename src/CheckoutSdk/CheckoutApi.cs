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
            PublicKey = configuration.PublicKey;
        }

        public IPaymentsClient Payments { get; }
        public ITokensClient Tokens { get; }
        public string PublicKey { get; }

        public static CheckoutApi Create(string secretKey, bool sandbox = true, string publicKey = null)
        {
            var configuration = new CheckoutConfiguration(secretKey, sandbox)
            {
                PublicKey = publicKey
            };

            var apiClient = new ApiClient(configuration);
            return new CheckoutApi(apiClient, configuration);
        }

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