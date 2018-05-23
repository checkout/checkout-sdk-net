using Checkout.Payments;

namespace Checkout
{
    public class CheckoutApi : ICheckoutApi
    {        
        public CheckoutApi(IApiClient apiClient)
        {
            Payments = new PaymentsClient(apiClient);
        }

        public IPaymentsClient Payments { get; }

        public static CheckoutApi Create(string secretKey, bool sandbox = true)
        {
            var apiClient = new ApiClient(new CheckoutConfiguration(secretKey, sandbox));
            return new CheckoutApi(apiClient);
        }

        public static CheckoutApi Create(string secretKey, string uri)
        {
            var apiClient = new ApiClient(new CheckoutConfiguration(secretKey, uri));
            return new CheckoutApi(apiClient);
        }
    }
}