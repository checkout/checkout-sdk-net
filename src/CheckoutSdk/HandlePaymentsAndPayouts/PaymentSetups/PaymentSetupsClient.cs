using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsClient : AbstractClient, IPaymentSetupsClient
    {
        private const string PaymentsPath = "payments";
        private const string SetupsPath = "setups";

        private const string ConfirmPath = "confirm";

        public PaymentSetupsClient(IApiClient apiClient, CheckoutConfiguration configuration)
                      : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Creates a Payment Setup
        /// </summary>
        public Task<PaymentSetupsResponse> CreatePaymentSetup(
            PaymentSetupsRequest paymentSetupsCreateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("paymentSetupsCreateRequest", paymentSetupsCreateRequest);
            return ApiClient.Post<PaymentSetupsResponse>(
                BuildPath(PaymentsPath, SetupsPath),
                SdkAuthorization(),
                paymentSetupsCreateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Updates a Payment Setup
        /// </summary>
        public Task<PaymentSetupsResponse> UpdatePaymentSetup(
            string id,
            PaymentSetupsRequest paymentSetupsUpdateRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id, "paymentSetupsUpdateRequest", paymentSetupsUpdateRequest);
            return ApiClient.Put<PaymentSetupsResponse>(
                BuildPath(PaymentsPath, SetupsPath, id),
                SdkAuthorization(),
                paymentSetupsUpdateRequest,
                cancellationToken
            );
        }

        /// <summary>
        /// Gets a Payment Setup
        /// </summary>
        public Task<PaymentSetupsResponse> GetPaymentSetup(
            string id,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id);
            return ApiClient.Get<PaymentSetupsResponse>(
                BuildPath(PaymentsPath, SetupsPath, id),
                SdkAuthorization(),
                cancellationToken
            );  
        }

        /// <summary>
        /// Confirms a Payment Setup
        /// </summary>
        public Task<PaymentSetupsConfirmResponse> ConfirmPaymentSetup(
            string id, 
            string paymentMethodOptionId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("id", id, "paymentMethodOptionId", paymentMethodOptionId);
            return ApiClient.Post<PaymentSetupsConfirmResponse>(
                BuildPath(PaymentsPath, SetupsPath, id, ConfirmPath, paymentMethodOptionId),
                SdkAuthorization(),
                cancellationToken
            );  
        }
    }
}