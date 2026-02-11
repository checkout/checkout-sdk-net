using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentMethods.Requests;
using Checkout.PaymentMethods.Responses;

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

            var queryFilter = new PaymentMethodsQueryFilter
            {
                ProcessingChannelId = processingChannelId
            };

            return ApiClient.Query<GetAvailablePaymentMethodsResponse>(
                PaymentMethodsPath,
                SdkAuthorization(),
                queryFilter,
                cancellationToken);
        }
    }
}