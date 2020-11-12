using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Events
{
    /// <summary>
    /// Default implementation of <see cref="IEventsClient"/>.
    /// </summary>
    public class EventsClient : IEventsClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;
        private const string path = "events";

        public EventsClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<AvailableEventTypesResponse> RetrieveEventTypes(CancellationToken cancellationToken = default(CancellationToken))
        {            
            return _apiClient.GetAsync<AvailableEventTypesResponse>("event-types", _credentials, cancellationToken);
        }

        public Task<EventResponse> RetrieveEvent(string eventId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<EventResponse>($"{path}/{eventId}", _credentials, cancellationToken);
        }

        public Task<EventNotificationResponse> RetrieveEventNotification(string eventId, string notificationId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<EventNotificationResponse>($"{path}/{eventId}/notifications/{notificationId}", _credentials, cancellationToken);
        }

        public Task<HttpResponseMessage> RetryAllWebhooks(string eventId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<HttpResponseMessage>($"{path}/{eventId}/webhooks/retry", _credentials, cancellationToken, null);
        }

        public Task<HttpResponseMessage> RetryWebhook(string eventId, string webhookId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<HttpResponseMessage>($"{path}/{eventId}/webhooks/{webhookId}/retry", _credentials, cancellationToken, null);
        }
    }
}
