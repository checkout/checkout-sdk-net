using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Events
{
    public class EventsClient : AbstractClient, IEventsClient
    {
        private const string EventsPath = "events";
        private const string WebhooksPath = "webhooks";
        private const string RetryPath = "retry";

        public EventsClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<List<EventTypesResponse>> RetrieveAllEventTypes(string version = null,
            CancellationToken cancellationToken = default)
        {
            var eventTypesPath = "event-types";
            if (!string.IsNullOrEmpty(version))
            {
                eventTypesPath = $"{eventTypesPath}?version={version}";
            }

            return ApiClient.Get<List<EventTypesResponse>>(eventTypesPath, SdkAuthorization(), cancellationToken);
        }

        public Task<EventsPageResponse> RetrieveEvents(RetrieveEventsRequest eventsRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("eventsRequest", eventsRequest);
            return ApiClient.Query<EventsPageResponse>(EventsPath, SdkAuthorization(), eventsRequest,
                cancellationToken);
        }

        public Task<EventResponse> RetrieveEvent(string eventId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("eventId", eventId);
            return ApiClient.Get<EventResponse>(BuildPath(EventsPath, eventId), SdkAuthorization(), cancellationToken);
        }

        public Task<EventNotificationResponse> RetrieveEventNotification(string eventId, string notificationId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("eventId", eventId, "notificationId", notificationId);
            return ApiClient.Get<EventNotificationResponse>(
                BuildPath(EventsPath, eventId, "notifications", notificationId),
                SdkAuthorization(), cancellationToken);
        }

        public Task<object> RetryWebhook(string eventId, string webhookId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("eventId", eventId, "webhookId", webhookId);
            return ApiClient.Post<object>(BuildPath(EventsPath, eventId, WebhooksPath, webhookId, RetryPath),
                SdkAuthorization(), null, cancellationToken);
        }

        public Task<object> RetryAllWebhooks(string eventId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("eventId", eventId);
            return ApiClient.Post<object>(BuildPath(EventsPath, eventId, WebhooksPath, RetryPath),
                SdkAuthorization(), null, cancellationToken);
        }
    }
}