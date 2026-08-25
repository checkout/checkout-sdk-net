using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Bacs
{
    /// <summary>
    /// Bacs Direct Debit client.
    /// </summary>
    public interface IBacsClient
    {
        /// <summary>
        /// Sends a Bacs Direct Debit pre-notification (advance notice) to a payer ahead of collecting
        /// funds from their account.
        /// </summary>
        /// <param name="bacsNotificationRequest">The pre-notification details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<BacsNotificationResponse> SendNotification(
            BacsNotificationRequest bacsNotificationRequest,
            CancellationToken cancellationToken = default);
    }
}
