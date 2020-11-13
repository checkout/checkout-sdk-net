using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Moq;
using Checkout.Events;
using System.Threading;
using System.Net.Http;
using System.Net;

namespace Checkout.Tests.Events
{
    public class RetryWebhookTests : ApiTestFixture
    {
        private readonly Mock<IEventsClient> _eventsClient;

        public RetryWebhookTests()
        {
            var canRetryAllWebhooksResponse = new CheckoutHttpResponseMessage<object>(HttpStatusCode.Accepted).MockHeaders();

            _eventsClient = new Mock<IEventsClient>();
            _eventsClient.Setup(eventsClient => eventsClient.RetryWebhook("evt_4ddvw5cfb4xurn3mfedxhdtvqa", "wh_fulbukihgg4ehjl7ew25ppyylq", default(CancellationToken))).ReturnsAsync(() => (canRetryAllWebhooksResponse.StatusCode, canRetryAllWebhooksResponse.Headers, canRetryAllWebhooksResponse.Content));
            _eventsClient.Setup(eventsClient => eventsClient.RetryWebhook(It.IsAny<string>(), It.IsNotIn(new string[] { "wh_fulbukihgg4ehjl7ew25ppyylq" }), default(CancellationToken))).ThrowsAsync(new CheckoutResourceNotFoundException("12345"));
        }

        [Fact]
        public async Task CanRetryWebhook()
        {
            var retryAllWebhooksResponse = await _eventsClient.Object.RetryWebhook("evt_4ddvw5cfb4xurn3mfedxhdtvqa", "wh_fulbukihgg4ehjl7ew25ppyylq");

            retryAllWebhooksResponse.ShouldNotBeNull();
            retryAllWebhooksResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
            retryAllWebhooksResponse.Headers.TryGetValues("Cko-Request-Id", out var ckoRequestId).ShouldBeTrue();
            retryAllWebhooksResponse.Headers.TryGetValues("Cko-Version", out var ckoVersion).ShouldBeTrue();
        }

        [Fact]
        public async Task Returns404ifEventDoesNotExist()
        {
           await Assert.ThrowsAsync<CheckoutResourceNotFoundException>(async () => await _eventsClient.Object.RetryWebhook("evt_isAny", "wh_doesNotExist"));
        }
    }
}
