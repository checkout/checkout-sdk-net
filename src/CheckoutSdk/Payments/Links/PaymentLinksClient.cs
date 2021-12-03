using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Links
{
    public class PaymentLinksClient : AbstractClient, IPaymentLinksClient
    {
        private const string PaymentLinksPath = "payment-links";

        public PaymentLinksClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<PaymentLinkDetailsResponse> Get(string reference, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("reference", reference);
            return ApiClient.Get<PaymentLinkDetailsResponse>(BuildPath(PaymentLinksPath, reference), SdkAuthorization(),
                cancellationToken);
        }

        public Task<PaymentLinkResponse> Create(PaymentLinkRequest paymentLinkRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentLinkRequest", paymentLinkRequest);
            return ApiClient.Post<PaymentLinkResponse>(PaymentLinksPath, SdkAuthorization(), paymentLinkRequest,
                cancellationToken, null);
        }
    }
}