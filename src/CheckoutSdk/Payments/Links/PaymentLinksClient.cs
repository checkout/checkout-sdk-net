using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Links
{
	public class PaymentLinksClient : AbstractClient, IPaymentLinksClient
	{

		private const string PaymentLinksPath = "/payment-links";

		public PaymentLinksClient(ApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
		{
		}

		public Task<PaymentLinkDetailsResponse> GetAsync(string reference, CancellationToken cancellationToken = default)
		{
			CheckoutUtils.ValidateParams("reference", reference);
			return ApiClient.Get<PaymentLinkDetailsResponse>(PaymentLinksPath + "/" + reference, SdkAuthorization(), cancellationToken);
		}

		public Task<PaymentLinkResponse> CreateAsync(PaymentLinkRequest paymentLinkRequest, CancellationToken cancellationToken = default)
		{
			CheckoutUtils.ValidateParams("paymentLinkRequest", paymentLinkRequest);
			return ApiClient.Post<PaymentLinkResponse>(PaymentLinksPath, SdkAuthorization(),paymentLinkRequest, cancellationToken, null);
		}
	}

}