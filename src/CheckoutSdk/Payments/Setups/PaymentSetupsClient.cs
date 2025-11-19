using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsClient : AbstractClient, IPaymentSetupsClient
    {
        private const string PaymentSetupPath = "payment-setups";
        private const string SetupsPath = "setups";

        public PaymentSetupsClient(IApiClient apiClient, CheckoutConfiguration configuration)
                      : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Creates a Payment Setup
        /// </summary>
        public Task<PaymentSetupsResponse> CreatePaymentSetup(
            PaymentSetupsCreatePaymentSetupRequest paymentSetupsCreatePaymentSetupRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentSetupsCreatePaymentSetupRequest", paymentSetupsCreatePaymentSetupRequest);
            return ApiClient.Post<PaymentSetupsResponse>(
                BuildPath(PaymentSetupPath, SetupsPath),
                SdkAuthorization(),
                paymentSetupsCreatePaymentSetupRequest,
                cancellationToken
            );
        }
    }
}