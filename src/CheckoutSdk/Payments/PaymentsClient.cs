using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    /// <summary>
    /// Default implementation of <see cref="IPaymentsClient"/>.
    /// </summary>
    public class PaymentsClient : IPaymentsClient
    {
        private static readonly Dictionary<HttpStatusCode, Type> PaymentResponseMappings = new Dictionary<HttpStatusCode, Type>
        {
            { HttpStatusCode.Accepted, typeof(PaymentPending)},
            { HttpStatusCode.Created, typeof(PaymentProcessed)}
        };

        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;
        private readonly ISerializer _serializer;

        public PaymentsClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : this(apiClient, configuration, new JsonSerializer())
        {

        }

        public PaymentsClient(IApiClient apiClient, CheckoutConfiguration configuration, ISerializer serializer)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);

            _serializer = serializer;
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, PaymentResponse Content)> RequestAPayment<TRequestSource>(PaymentRequest<TRequestSource> paymentRequest, CancellationToken cancellationToken = default(CancellationToken), string idempotencyKey = null) 
            where TRequestSource : IRequestSource
        {
            return RequestPaymentAsync(paymentRequest, PaymentResponseMappings, cancellationToken, idempotencyKey);
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, GetPaymentResponse Content)> GetPaymentDetails(string paymentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<GetPaymentResponse>(GetPaymentUrl(paymentId), _credentials, cancellationToken);
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, IEnumerable<PaymentAction> Content)> GetPaymentActions(string paymentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            const string path = "/actions";
            return _apiClient.GetAsync<IEnumerable<PaymentAction>>(GetPaymentUrl(paymentId) + path, _credentials, cancellationToken);
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, CaptureResponse Content)> CaptureAPayment(string paymentId, CaptureRequest captureRequest = null, CancellationToken cancellationToken = default(CancellationToken), string idempotencyKey = null)
        {
            const string path = "/captures";
            return _apiClient.PostAsync<CaptureResponse>(GetPaymentUrl(paymentId) + path, _credentials, cancellationToken, captureRequest, idempotencyKey);
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, RefundResponse Content)> RefundAPayment(string paymentId, RefundRequest refundRequest = null, CancellationToken cancellationToken = default(CancellationToken), string idempotencyKey = null)
        {
            const string path = "/refunds";
            return _apiClient.PostAsync<RefundResponse>(GetPaymentUrl(paymentId) + path, _credentials, cancellationToken, refundRequest, idempotencyKey);
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, VoidResponse Content)> VoidAPayment(string paymentId, VoidRequest voidRequest = null, CancellationToken cancellationToken = default(CancellationToken), string idempotencyKey = null)
        {
            const string path = "/voids";
            return _apiClient.PostAsync<VoidResponse>(GetPaymentUrl(paymentId) + path, _credentials, cancellationToken, voidRequest, idempotencyKey);
        }

        private async Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, PaymentResponse Content)> RequestPaymentAsync<TRequestSource>(PaymentRequest<TRequestSource> paymentRequest, Dictionary<HttpStatusCode, Type> resultTypeMappings, CancellationToken cancellationToken, string idempotencyKey) where TRequestSource : IRequestSource
        {
            const string path = "payments";
            var (StatusCode, Headers, Content) = await _apiClient.PostAsync(path, _credentials, resultTypeMappings, cancellationToken, paymentRequest, idempotencyKey);
            return (StatusCode, Headers, _serializer.Deserialize(_serializer.Serialize(Content), (Content as object).GetType()));
        }

        private static string GetPaymentUrl(string paymentId)
        {
            const string path = "payments/";
            return path + paymentId;
        }
    }
}
