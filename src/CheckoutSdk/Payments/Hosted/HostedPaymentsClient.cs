using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsClient : AbstractClient, IHostedPaymentsClient
	{
		private const string HOSTED_PAYMENTS = "/hosted-payments";		

		public HostedPaymentsClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
		{
			
		}

		public virtual Task<HostedPaymentResponse> CreateAsync(HostedPaymentRequest hostedPaymentRequest, CancellationToken cancellationToken = default)
		{
			return ApiClient.Post<HostedPaymentResponse>(BuildPath(HOSTED_PAYMENTS), SdkAuthorization(), hostedPaymentRequest, cancellationToken);
		}
	}

}