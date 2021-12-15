using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsClient : AbstractClient, IHostedPaymentsClient
    {
        private const string HostedPaymentsPath = "hosted-payments";

        public HostedPaymentsClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<HostedPaymentDetailsResponse> Get(string hostedPaymentId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("hostedPaymentId", hostedPaymentId);
            return ApiClient.Get<HostedPaymentDetailsResponse>(BuildPath(HostedPaymentsPath, hostedPaymentId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<HostedPaymentResponse> Create(HostedPaymentRequest hostedPaymentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("hostedPaymentRequest", hostedPaymentRequest);
            return ApiClient.Post<HostedPaymentResponse>(HostedPaymentsPath, SdkAuthorization(), hostedPaymentRequest,
                cancellationToken);
        }
    }
}