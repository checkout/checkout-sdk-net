using System;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private readonly IApiClient _apiClient;

        public PaymentsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }
        
        public Task<ApiResponse<CardPaymentResponse>> RequestAsync(CardPaymentRequest cardPaymentRequest)
        {
            if (cardPaymentRequest == null) throw new ArgumentNullException(nameof(cardPaymentRequest));
            return _apiClient.PostAsync<CardPaymentRequest, CardPaymentResponse>("/payments", cardPaymentRequest);
        }

        public static PaymentsClient Create(string secretKey, bool sandbox = true)
        {
            var configuration = new CheckoutConfiguration(secretKey, sandbox);
            var apiClient = new ApiClient(configuration);
            return new PaymentsClient(apiClient);
        }
    }
}