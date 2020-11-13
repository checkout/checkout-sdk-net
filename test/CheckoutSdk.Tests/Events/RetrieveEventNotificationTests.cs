using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Moq;
using Checkout.Events;
using System;
using System.Collections.Generic;
using Checkout.Common;
using System.Threading;
using System.Net;

namespace Checkout.Tests.Events
{
    public class RetrieveEventNotificationTests : ApiTestFixture
    {
        private readonly Mock<IEventsClient> _eventsClient;

        public RetrieveEventNotificationTests()
        {
            var eventNotificationResponse = new EventNotificationResponse()
            {
                Id = "ntf_cuzguxmkcncu5g2tmzghsnbkm4",
                Url = "https://www.example.com/webhooks/incoming/checkout",
                Success = true,
                ContentType = "json",
                Attempts = new List<EventNotificationAttempt>()
                {
                    new EventNotificationAttempt()
                    {
                        StatusCode = 200,
                        SendMode = "automatic",
                        Timestamp = DateTime.Parse("2020-09-15T08:00:29Z")
                    }
                },
                Links = new Dictionary<string, Link>()
                    {
                        { "self", new Link(){ Href = "https://api.sandbox.checkout.com/events/evt_g7danjfojdse5hclmq2pk6csve/notifications/ntf_cuzguxmkcncu5g2tmzghsnbkm4" } },
                        { "webhook-retry", new Link(){ Href = "https://api.sandbox.checkout.com/events/evt_g7danjfojdse5hclmq2pk6csve/webhooks/wh_xttkznlbnf4ehezf4exbszlql4/retry" } }
                    }
            }; ;
            var canRetrieveEventNotificationResponse = new CheckoutHttpResponseMessage<EventNotificationResponse>(HttpStatusCode.OK, eventNotificationResponse).MockHeaders();

            _eventsClient = new Mock<IEventsClient>();
            _eventsClient.Setup(eventsClient => eventsClient.RetrieveEventNotification("evt_g7danjfojdse5hclmq2pk6csve", "ntf_cuzguxmkcncu5g2tmzghsnbkm4", default(CancellationToken))).ReturnsAsync(() => canRetrieveEventNotificationResponse);
            _eventsClient.Setup(eventsClient => eventsClient.RetrieveEventNotification(It.IsAny<string>(), It.IsNotIn(new string[] { "ntf_cuzguxmkcncu5g2tmzghsnbkm4" }), default(CancellationToken))).ThrowsAsync(new CheckoutResourceNotFoundException("12345"));
        }

        [Fact]
        public async Task CanRetrieveEventNotification()
        {
            var eventNotificationRetrievalResponse = await _eventsClient.Object.RetrieveEventNotification(eventId: "evt_g7danjfojdse5hclmq2pk6csve", notificationId: "ntf_cuzguxmkcncu5g2tmzghsnbkm4");

            eventNotificationRetrievalResponse.Content.ShouldNotBeNull();
            eventNotificationRetrievalResponse.Content.ShouldBeOfType<EventNotificationResponse>();
            eventNotificationRetrievalResponse.Content.Attempts.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Returns404ifEventNotificationDoesNotExist()
        {
            await Assert.ThrowsAsync<CheckoutResourceNotFoundException>(async () => await _eventsClient.Object.RetrieveEventNotification("evt_isAny", "ntf_doesNotExist"));
        }
    }
}
