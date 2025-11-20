using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsClient : AbstractClient, IPaymentSetupsClient
    {
        private const string PaymentsPath = "payments";
        private const string SetupsPath = "setups";

        public PaymentSetupsClient(IApiClient apiClient, CheckoutConfiguration configuration)
                      : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Creates a Payment Setup
        /// </summary>
        public Task<PaymentSetupsResponse> CreatePaymentSetup(
            PaymentSetupsCreateRequest paymentSetupsCreatePaymentSetupRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentSetupsCreatePaymentSetupRequest", paymentSetupsCreatePaymentSetupRequest);
            return ApiClient.Post<PaymentSetupsResponse>(
                BuildPath(PaymentsPath, SetupsPath),
                SdkAuthorization(),
                paymentSetupsCreatePaymentSetupRequest,
                cancellationToken
            );
        }
    }
}