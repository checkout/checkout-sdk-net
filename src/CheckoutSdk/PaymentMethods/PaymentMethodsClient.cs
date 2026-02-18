using Checkout.PaymentMethods.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentMethods
{
    public class PaymentMethodsClient : AbstractClient, IPaymentMethodsClient
    {
        private const string PaymentMethodsPath = "payment-methods";

        public PaymentMethodsClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<GetAvailablePaymentMethodsResponse> GetAvailablePaymentMethods(
            string processingChannelId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("processingChannelId", processingChannelId);

            return ApiClient.Get<GetAvailablePaymentMethodsResponse>(
                BuildPath(PaymentMethodsPath, processingChannelId),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}