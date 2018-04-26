using System.Threading.Tasks;

namespace Checkout.Payments
{
    public class PaymentsClient : IPaymentOperations
    {
        private readonly IApiClient _apiClient;

        public PaymentsClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<ApiResponse<CardPaymentResponse>> RequestAsync(CardPaymentRequest cardPaymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<CardPaymentResponse>> RequestAsync(TokenPaymentRequest tokenPaymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<AlternativePaymentResponse>> RequestAsync(AlternativePaymentRequest alternativePaymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<AlternativePaymentResponse>> RequestAsync<TRequest>(TRequest alternativePaymentRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}