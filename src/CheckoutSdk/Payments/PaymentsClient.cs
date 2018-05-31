using System;
using System.Collections.Generic;
using System.Net;
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

        public Task<ApiResponse<CaptureResponse>> CaptureAsync(string paymentId, CaptureRequest captureRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<CardSource> request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}