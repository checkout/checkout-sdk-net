using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsClient : AbstractClient, IPaymentContextsClient
    {
        private const string PaymentContextsPath = "payment-contexts";

        public PaymentContextsClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<PaymentContextDetailsResponse> GetPaymentContextDetails(string paymentContextId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentContextId", paymentContextId);
            return ApiClient.Get<PaymentContextDetailsResponse>(
                BuildPath(PaymentContextsPath, paymentContextId),
                SdkAuthorization(),
                cancellationToken
            );
        }

        public Task<PaymentContextsRequestResponse> RequestPaymentContexts(
            PaymentContextsRequest paymentContextsRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentContextsRequest", paymentContextsRequest);
            return ApiClient.Post<PaymentContextsRequestResponse>(
                PaymentContextsPath,
                SdkAuthorization(),
                paymentContextsRequest,
                cancellationToken
            );
        }
    }
}