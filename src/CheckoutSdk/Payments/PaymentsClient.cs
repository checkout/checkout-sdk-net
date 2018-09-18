using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    public class PaymentsClient : IPaymentsClient
    {
        private static readonly Dictionary<HttpStatusCode, Type> CardPaymentMappings = new Dictionary<HttpStatusCode, Type>
        {
            { HttpStatusCode.Accepted, typeof(PaymentPending)},
            { HttpStatusCode.Created, typeof(PaymentProcessed)}
        };

        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;

        public PaymentsClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            this._apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<PaymentResponse> RequestAsync<TRequestSource>(PaymentRequest<TRequestSource> paymentRequest, CancellationToken cancellationToken = default(CancellationToken)) 
            where TRequestSource : IPaymentSource
        {
            return RequestPaymentAsync(paymentRequest, CardPaymentMappings, cancellationToken);
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

        private async Task<PaymentResponse> RequestPaymentAsync<TRequestSource>(
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

        public Task<ICollection<Action>> GetActionsAsync(string paymentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<ICollection<Action>>(GetPaymentUrl(paymentId) + "/actions", _credentials, cancellationToken);
        }

        private static string GetPaymentUrl(string paymentId)
        {
            return "payments/" + paymentId;
        }
    }
}