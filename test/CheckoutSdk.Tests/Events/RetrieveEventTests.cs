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

namespace Checkout.Tests.Events
{
    public class RetrieveEventTests : ApiTestFixture
    {
        private readonly Mock<IEventsClient> _eventsClient;
        private readonly EventResponse _eventResponse;

        public RetrieveEventTests()
        {
            _eventResponse = new EventResponse()
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
            _eventsClient = new Mock<IEventsClient>();
            _eventsClient.Setup(eventsClient => eventsClient.RetrieveEventAsync("evt_c2qelfixai2u3es3ksovngkx3e", default(CancellationToken))).ReturnsAsync(() => _eventResponse);
            _eventsClient.Setup(eventsClient => eventsClient.RetrieveEventAsync(It.IsNotIn(new string[] { "evt_c2qelfixai2u3es3ksovngkx3e" }), default(CancellationToken))).ThrowsAsync(new CheckoutResourceNotFoundException("12345"));
        }

        [Fact]
        public async Task CanRetrieveEvent()
        {
            var eventRetrievalResponse = await _eventsClient.Object.RetrieveEventAsync("evt_c2qelfixai2u3es3ksovngkx3e");

            eventRetrievalResponse.ShouldNotBeNull();
            eventRetrievalResponse.ShouldBeOfType<EventResponse>();
        }

        [Fact]
        public async Task Returns404ifEventDoesNotExist()
        {
            await Assert.ThrowsAsync<CheckoutResourceNotFoundException>(async () => await _eventsClient.Object.RetrieveEventAsync("evt_doesNotExist"));
        }
    }
}
