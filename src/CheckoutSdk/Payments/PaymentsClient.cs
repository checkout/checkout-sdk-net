using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Checkout.Payments.Request;
using Checkout.Payments.Response;

namespace Checkout.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private readonly IApiClient _apiClient;

        public PaymentsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public Task<ApiResponse<PaymentResponse<Response.CardSource>>> RequestAsync(PaymentRequest<Request.CardSource> request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PaymentResponse<Response.CardSource>>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}