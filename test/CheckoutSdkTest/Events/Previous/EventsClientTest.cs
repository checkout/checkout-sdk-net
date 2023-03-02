using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Events.Previous
{
    public class EventsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public EventsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);
            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldRetrieveAllEventTypes()
        {
            var eventTypesResponse = new ItemsResponse<EventTypesResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<EventTypesResponse>>("event-types?version=1.0", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => eventTypesResponse);

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveAllEventTypes("1.0");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(eventTypesResponse);
        }

        [Fact]
        private async Task ShouldRetrieveAllEventTypesWithoutVersion()
        {
            var eventTypesResponse = new ItemsResponse<EventTypesResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<EventTypesResponse>>("event-types", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => eventTypesResponse);

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveAllEventTypes();

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(eventTypesResponse);
        }

        [Fact]
        private async Task ShouldRetrieveEvents()
        {
            var request = new RetrieveEventsRequest();
            var eventsPageResponse = new EventsPageResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<EventsPageResponse>("events", _authorization, request, CancellationToken.None))
                .ReturnsAsync(() => eventsPageResponse);

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveEvents(request);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(eventsPageResponse);
        }

        [Fact]
        private async Task ShouldRetrieveEvent()
        {
            var eventResponse = new EventResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<EventResponse>("events/event_id", _authorization, CancellationToken.None))
                .ReturnsAsync(() => eventResponse);

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveEvent("event_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(eventResponse);
        }

        [Fact]
        private async Task ShouldRetrieveEventNotification()
        {
            var notificationResponse = new EventNotificationResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<EventNotificationResponse>("events/event_id/notifications/notification_id",
                        _authorization, CancellationToken.None))
                .ReturnsAsync(() => notificationResponse);

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetrieveEventNotification("event_id", "notification_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(notificationResponse);
        }

        [Fact]
        private async Task ShouldRetryWebhook()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<EmptyResponse>("events/event_id/webhooks/webhook_id/retry", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new EmptyResponse());

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetryWebhook("event_id", "webhook_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetryAllWebhooks()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<EmptyResponse>("events/event_id/webhooks/retry", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new EmptyResponse());

            IEventsClient client = new EventsClient(_apiClient.Object, _configuration.Object);

            var response = await client.RetryAllWebhooks("event_id");

            response.ShouldNotBeNull();
        }
    }
}