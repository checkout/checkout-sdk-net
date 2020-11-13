using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Moq;
using Checkout.Events;
using System;
using System.Collections.Generic;
using Checkout.Payments;
using Checkout.Common;
using System.Threading;
using System.Net;

namespace Checkout.Tests.Events
{
    public class RetrieveEventTests : ApiTestFixture
    {
        private readonly Mock<IEventsClient> _eventsClient;

        public RetrieveEventTests()
        {
            var eventResponse = new EventResponse()
            {
                Id = "evt_c2qelfixai2u3es3ksovngkx3e",
                Type = "payment_captured",
                Version = "2.0",
                CreatedOn = DateTime.Parse("2020-04-24T11:50:11Z"),
                Data = new Dictionary<string, object>()
                {
                    {"action_id", "act_puqzoc4sna7uvhjledmp6vjjby" },
                    {"response_code", "10000" },
                    {"response_summary", "Approved" },
                    {"amount", 1000 },
                    {"metadata", new Dictionary<string, string>() },
                    {"processing", new ProcessingResponse()
                        {
                            AcquirerTransactionId = "1159094323",
                            RetrievalReferenceNumber = "874479685178"
                        }
                    },
                    {"id", "pay_xskg4u47uabenhpxfvpmqduume" },
                    {"currency", "USD" },
                    {"processed_on", DateTime.Parse("2020-04-24T11:50:11Z") }
                },
                Notifications = new List<EventNotificationSummary>(),
                Links = new Dictionary<string, Link>()
                    {
                        { "self", new Link(){ Href = "https://api.sandbox.checkout.com/events/evt_c2qelfixai2u3es3ksovngkx3e" } },
                        { "webhooks-retry", new Link(){ Href = "https://api.sandbox.checkout.com/events/evt_c2qelfixai2u3es3ksovngkx3e/webhooks/retry" } }
                    }
            };
            var canRetrieveEventResponse = new CheckoutHttpResponseMessage<EventResponse>(HttpStatusCode.OK, eventResponse).MockHeaders();

            _eventsClient = new Mock<IEventsClient>();
            _eventsClient.Setup(eventsClient => eventsClient.RetrieveEvent("evt_c2qelfixai2u3es3ksovngkx3e", default(CancellationToken))).ReturnsAsync(() => (canRetrieveEventResponse.StatusCode, canRetrieveEventResponse.Headers, canRetrieveEventResponse.Content));
            _eventsClient.Setup(eventsClient => eventsClient.RetrieveEvent(It.IsNotIn(new string[] { "evt_c2qelfixai2u3es3ksovngkx3e" }), default(CancellationToken))).ThrowsAsync(new CheckoutResourceNotFoundException("12345"));
        }

        [Fact]
        public async Task CanRetrieveEvent()
        {
            var eventRetrievalResponse = await _eventsClient.Object.RetrieveEvent("evt_c2qelfixai2u3es3ksovngkx3e");

            eventRetrievalResponse.Content.ShouldNotBeNull();
            eventRetrievalResponse.Content.ShouldBeOfType<EventResponse>();
        }

        [Fact]
        public async Task Returns404ifEventDoesNotExist()
        {
            await Assert.ThrowsAsync<CheckoutResourceNotFoundException>(async () => await _eventsClient.Object.RetrieveEvent("evt_doesNotExist"));
        }
    }
}
