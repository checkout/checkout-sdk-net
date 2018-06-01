using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private static Dictionary<HttpStatusCode, Type> CardPaymentMappings = new Dictionary<HttpStatusCode, Type>
        {
            { HttpStatusCode.Accepted, typeof(PaymentPending)},
            { HttpStatusCode.Created, typeof(PaymentProcessed<CardSourceResponse>)}
        };

        private readonly IApiClient _apiClient;

        public PaymentsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<CardSource> cardPaymentRequest)
        {
            return RequestPaymentAsync<CardSource, CardSourceResponse>(cardPaymentRequest, CardPaymentMappings);
        }

        public Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest)
        {
            return RequestPaymentAsync<TokenSource, CardSourceResponse>(tokenPaymentRequest, CardPaymentMappings);
        }

        public Task<ApiResponse<VoidResponse>> VoidAsync(string paymentId, VoidRequest voidRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CaptureResponse>> CaptureAsync(string paymentId, CaptureRequest captureRequest = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<RefundResponse>> RefundAsync(string paymentId, RefundRequest refundRequest = null)
        {
            throw new NotImplementedException();
        }

        private async Task<ApiResponse<PaymentResponse<TResponseSource>>> RequestPaymentAsync<TRequestSource, TResponseSource>(
            PaymentRequest<TRequestSource> paymentRequest,
            Dictionary<HttpStatusCode, Type> resultTypeMappings) where TRequestSource : IPaymentSource
        {
            var apiResponse = await _apiClient.PostAsync("payments", paymentRequest, resultTypeMappings);

            return new ApiResponse<PaymentResponse<TResponseSource>>
            {
                StatusCode = apiResponse.StatusCode,
                Error = apiResponse.Error,
                Result = apiResponse.Result
            };
        }
    }
}