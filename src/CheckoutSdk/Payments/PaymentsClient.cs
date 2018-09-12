using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sdk.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private static Dictionary<HttpStatusCode, Type> CardPaymentMappings = new Dictionary<HttpStatusCode, Type>
        {
            { HttpStatusCode.Accepted, typeof(PaymentPending)},
            { HttpStatusCode.Created, typeof(PaymentProcessed<CardSourceResponse>)}
        };

        private readonly IApiClient _apiClient;
        private IApiCredentials _credentials;

        public PaymentsClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<PaymentResponse<CardSourceResponse>> RequestAsync(PaymentRequest<CardSource> cardPaymentRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestPaymentAsync<CardSource, CardSourceResponse>(cardPaymentRequest, CardPaymentMappings, cancellationToken);
        }

        public Task<PaymentResponse<CardSourceResponse>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestPaymentAsync<TokenSource, CardSourceResponse>(tokenPaymentRequest, CardPaymentMappings, cancellationToken);
        }

        public Task<VoidResponse> VoidAsync(string paymentId, VoidRequest voidRequest = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<VoidResponse>(GetPaymentUrl(paymentId) + "/voids", _credentials, cancellationToken, voidRequest);
        }

        public Task<CaptureResponse> CaptureAsync(string paymentId, CaptureRequest captureRequest = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<CaptureResponse>(GetPaymentUrl(paymentId) + "/captures", _credentials, cancellationToken, captureRequest);
        }

        public Task<RefundResponse> RefundAsync(string paymentId, RefundRequest refundRequest = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<RefundResponse>(GetPaymentUrl(paymentId) + "/refunds", _credentials, cancellationToken, refundRequest);
        }

        private async Task<PaymentResponse<TResponseSource>> RequestPaymentAsync<TRequestSource, TResponseSource>(
            PaymentRequest<TRequestSource> paymentRequest,
            Dictionary<HttpStatusCode, Type> resultTypeMappings, CancellationToken cancellationToken) where TRequestSource : IPaymentSource
        {
            var apiResponse = await _apiClient.PostAsync("payments", _credentials, paymentRequest, resultTypeMappings, cancellationToken);
            return apiResponse;
        }

        public Task<GetPaymentResponse> GetAsync(string paymentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<GetPaymentResponse>(GetPaymentUrl(paymentId), _credentials, cancellationToken);
        }

        private static string GetPaymentUrl(string paymentId)
        {
            return "payments/" + paymentId;
        }
    }
}