using Checkout.AgenticCommerce.Requests;
using Checkout.AgenticCommerce.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.AgenticCommerce
{
    public class AgenticCommerceClient : AbstractClient, IAgenticCommerceClient
    {
        private const string AgenticCommercePath = "agentic_commerce";
        private const string DelegatePaymentPath = "delegate_payment";

        public AgenticCommerceClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<DelegatedPaymentResponse> CreateDelegatedPaymentToken(DelegatedPaymentRequest request,
            DelegatedPaymentHeaders headers,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request, "headers", headers);
            return ApiClient.Post<DelegatedPaymentResponse>(
                BuildPath(AgenticCommercePath, DelegatePaymentPath),
                SdkAuthorization(),
                request,
                cancellationToken,
                idempotencyKey,
                headers);
        }
    }
}
