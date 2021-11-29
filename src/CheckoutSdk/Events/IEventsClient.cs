using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Events
{
    public interface IEventsClient
    {
        Task<List<EventTypesResponse>> RetrieveAllEventTypes(string version = null,
            CancellationToken cancellationToken = default);

        Task<EventsPageResponse> RetrieveEvents(RetrieveEventsRequest eventsRequest,
            CancellationToken cancellationToken = default);

        Task<EventResponse> RetrieveEvent(string eventId, CancellationToken cancellationToken = default);

        Task<EventNotificationResponse> RetrieveEventNotification(string eventId, string notificationId,
            CancellationToken cancellationToken = default);

        Task<object> RetryWebhook(string eventId, string webhookId,
            CancellationToken cancellationToken = default);

        Task<object> RetryAllWebhooks(string eventId, CancellationToken cancellationToken = default);
    }
}