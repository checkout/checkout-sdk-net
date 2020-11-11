using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Moq;
using Checkout.Events;
using System.Threading;
using System.Net.Http;

namespace Checkout.Tests.Events
{
    public class RetryAllWebhooksTests : ApiTestFixture
    {
        private readonly Mock<IEventsClient> _eventsClient;
        private readonly HttpResponseMessage _acceptedResponse;

        public RetryAllWebhooksTests()
        {
            _acceptedResponse = new CheckoutAcceptedApiResponse();
            _acceptedResponse.Headers.Add("Cko-Request-Id", "0HM2PGFFI1ND9:000002FA");
            _acceptedResponse.Headers.Add("Cko-Version", "2.12.0");

            _eventsClient = new Mock<IEventsClient>();
            _eventsClient.Setup(eventsClient => eventsClient.RetryAllWebhooksAsync("evt_4ddvw5cfb4xurn3mfedxhdtvqa", default(CancellationToken))).ReturnsAsync(() => _acceptedResponse);
            _eventsClient.Setup(eventsClient => eventsClient.RetryAllWebhooksAsync(It.IsNotIn(new string[] { "evt_4ddvw5cfb4xurn3mfedxhdtvqa" }), default(CancellationToken))).ThrowsAsync(new CheckoutResourceNotFoundException("12345"));
        }

        [Fact]
        public async Task CanRetryAllWebhooks()
        {
            var retryAllWebhooksResponse = await _eventsClient.Object.RetryAllWebhooksAsync("evt_4ddvw5cfb4xurn3mfedxhdtvqa");

            retryAllWebhooksResponse.ShouldNotBeNull();
            retryAllWebhooksResponse.ShouldBeOfType<CheckoutAcceptedApiResponse>();
            retryAllWebhooksResponse.Headers.TryGetValues("Cko-Request-Id", out var ckoRequestId).ShouldBeTrue();
            retryAllWebhooksResponse.Headers.TryGetValues("Cko-Version", out var ckoVersion).ShouldBeTrue();
        }

        [Fact]
        public async Task Returns404ifEventDoesNotExist()
        {
           await Assert.ThrowsAsync<CheckoutResourceNotFoundException>(async () => await _eventsClient.Object.RetryAllWebhooksAsync("evt_doesNotExist"));
        }
    }
}
