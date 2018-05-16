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

        public static CheckoutApi Create(string secretKey, bool useSandbox = true)
        {
            var configuration = new CheckoutConfiguration(secretKey, useSandbox);
            var apiClient = new ApiClient(configuration);
            return new CheckoutApi(apiClient);
        }
    }
}