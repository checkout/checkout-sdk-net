using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Sessions
{
    public class PaymentSessionsClient : AbstractClient, IPaymentSessionsClient
    {
        private const string PaymentSessionsPath = "payment-sessions";

        public PaymentSessionsClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<PaymentSessionsResponse> RequestPaymentSessions(
            PaymentSessionsRequest paymentSessionsRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentSessionsRequest", paymentSessionsRequest);
            return ApiClient.Post<PaymentSessionsResponse>(
                PaymentSessionsPath,
                SdkAuthorization(),
                paymentSessionsRequest,
                cancellationToken
            );
        }
    }
}