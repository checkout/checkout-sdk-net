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

        public Task<PaymentResponse<PaymentSource>> GetPaymentAsync(string paymentToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<CardPaymentResponse> RequestAsync(CardPaymentRequest cardPaymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<CardPaymentResponse> RequestAsync(TokenPaymentRequest tokenPaymentRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<AlternativePaymentResponse> RequestAsync<TRequest>(TRequest alternativePaymentRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}