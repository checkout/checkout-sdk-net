using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Events
{
    /// <summary>
    /// Defines the operations available on the Checkout.com Events API.
    /// </summary>
    public interface IEventsClient
    {
        /// <summary>
        /// Retrieves the available event types
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        /// <returns>A task that upon completion contains the available event types.</returns>
        Task<AvailableEventTypesResponse> RetrieveEventTypesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves a specific event
        /// </summary>
        /// <param name="id">The unique identifier of the event to be retrieved.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        /// <returns>A task that upon completion contains the details of a specific event.</returns>
        Task<EventResponse> RetrieveEventAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves a specific notification of a specific event
        /// </summary>
        /// <param name="eventId">The unique identifier of the event that triggered the notification.</param>
        /// <param name="notificationId">The unique identifier of the notification to be retrieved.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        /// <returns>A task that upon completion contains the details of a specific notification of a specific event.</returns>
        Task<EventNotificationResponse> RetrieveEventNotificationAsync(string eventId, string notificationId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retries all webhooks for a specific event
        /// </summary>
        /// <param name="id">The unique identifier of the event for which to retry all webhooks.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        Task<HttpResponseMessage> RetryAllWebhooksAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retries a specific webhook for a specific event
        /// </summary>
        /// <param name="eventId">The unique identifier of the event that triggered the notification.</param>
        /// <param name="webhookId">The unique identifier of the webhook to be retried.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the underlying HTTP request.</param>
        Task<HttpResponseMessage> RetryWebhookAsync(string eventId, string webhookId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
