using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Bacs
{
    /// <summary>
    /// Bacs Direct Debit client.
    /// </summary>
    public class BacsClient : AbstractClient, IBacsClient
    {
        private const string ApmsPath = "apms";
        private const string BacsPath = "bacs";
        private const string NotificationsPath = "notifications";

        public BacsClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<BacsNotificationResponse> SendNotification(
            BacsNotificationRequest bacsNotificationRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("bacsNotificationRequest", bacsNotificationRequest);
            return ApiClient.Post<BacsNotificationResponse>(
                BuildPath(ApmsPath, BacsPath, NotificationsPath),
                SdkAuthorization(),
                bacsNotificationRequest,
                cancellationToken);
        }
    }
}
