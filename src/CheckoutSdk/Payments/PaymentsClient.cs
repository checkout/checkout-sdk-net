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
            return _apiClient.PostAsync<CardPaymentRequest, CardPaymentResponse>("payments", cardPaymentRequest);
        }
    }
}