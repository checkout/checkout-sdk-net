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
        /// Creates a Payment Setup.
        /// To maximize the amount of information the payment setup can use, we recommend that you create a payment
        /// setup as early as possible in the customer's journey. For example, the first time they land on the basket
        /// page
        /// [Beta]
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
        /// You should update the payment setup whenever there are significant changes in the data relevant to the
        /// customer's transaction. For example, when the customer makes a change that impacts the total payment amount
        /// [Beta]
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
        /// Retrieves a Payment Setup
        /// [Beta]
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
        /// Confirm a Payment Setup to begin processing the payment request with your chosen payment method option
        /// [Beta]
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