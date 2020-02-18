using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    /// <summary>
    /// Defines the operations available on the Checkout.com Disputes API.
    /// </summary>
    public interface IDisputesClient
    {
        /// <summary>
        /// Returns all disputes on either business or channel level.
        /// Response can be filtered via several parameters.
        /// </summary>
        /// <param name="limit">The numbers of results to return. 1 .. 250, default is 50.</param>
        /// <param name="skip">The number of results to skip.</param>
        /// <param name="from">The ISO-8601 date and time from which to filter disputes, based on the dispute's last_update field.</param>
        /// <param name="to">The ISO-8601 date and time until which to filter disputes, based on the dispute's last_update field</param>
        /// <param name="id">The unique identifier of the dispute.</param>
        /// <param name="statuses">One or more comma-separated statuses. This works like a logical OR operator.</param>
        /// <param name="paymentId">The unique identifier of the payment.</param>
        /// <param name="paymentReference">An optional reference (such as an order ID) that you can use later to identify the payment.</param>
        /// <param name="paymentArn">The acquirer reference number (ARN) that you can use to query the issuing bank.</param>
        /// <param name="thisChannelOnly">If true, only returns disputes of the specific channel that the secret key is associated with. Otherwise, returns all disputes for that business.</param>
        /// <returns>A task that upon completion contains the payment details.</returns>
        Task<GetDisputesResponse> GetDisputesAsync(
            int? limit = null,
            int? skip = null,
            string from = "",
            string to = "",
            string id = "",
            string statuses = "",
            string paymentId = "",
            string paymentReference = "",
            string paymentArn = "",
            bool? thisChannelOnly = null,
            CancellationToken cancellationToken = default(CancellationToken)
            );
    }
}
