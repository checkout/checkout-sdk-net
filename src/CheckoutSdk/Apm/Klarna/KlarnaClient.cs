using System.Threading;
using System.Threading.Tasks;
using Checkout.Payments;

namespace Checkout.Apm.Klarna
{
    public class KlarnaClient : AbstractClient, IKlarnaClient
    {
        private const string CreditSessionsPath = "credit-sessions";
        private const string OrdersPath = "orders";
        private const string CapturesPath = "captures";
        private const string VoidsPath = "voids";

        public KlarnaClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.PublicKey)
        {
        }

        public Task<CreditSessionResponse> CreateCreditSession(CreditSessionRequest creditSessionRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("creditSessionRequest", creditSessionRequest);
            return ApiClient.Post<CreditSessionResponse>(
                BuildPath(GetBaseUrl(), CreditSessionsPath),
                SdkAuthorization(),
                creditSessionRequest,
                cancellationToken);
        }

        public Task<CreditSession> GetCreditSession(string sessionId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionId", sessionId);
            return ApiClient.Get<CreditSession>(BuildPath(GetBaseUrl(), CreditSessionsPath, sessionId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<CaptureResponse> CapturePayment(string paymentId, OrderCaptureRequest orderCaptureRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId, "orderCaptureRequest", orderCaptureRequest);
            return ApiClient.Post<CaptureResponse>(
                BuildPath(GetBaseUrl(), OrdersPath, paymentId, CapturesPath),
                SdkAuthorization(),
                orderCaptureRequest,
                cancellationToken);
        }

        public Task<VoidResponse> VoidPayment(string paymentId, VoidRequest voidRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentId", paymentId, "voidRequest", voidRequest);
            return ApiClient.Post<VoidResponse>(
                BuildPath(GetBaseUrl(), OrdersPath, paymentId, VoidsPath),
                SdkAuthorization(),
                voidRequest,
                cancellationToken);
        }

        private string GetBaseUrl()
        {
            return IsSandbox() ? "klarna-external" : "klarna";
        }
    }
}