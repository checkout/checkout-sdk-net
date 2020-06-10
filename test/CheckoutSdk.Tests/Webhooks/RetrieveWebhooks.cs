using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Webhooks
{
    public class RetrieveWebhooksTests : ApiTestFixture
    {
        [Fact]
        public async Task CanRetrieveWebhooks()
        {
            var webhooksRetrievalResponse = await Api.Webhooks.RetrieveWebhooksAsync();

            webhooksRetrievalResponse.ShouldNotBeNull();
            webhooksRetrievalResponse.Count.ShouldBeGreaterThan(0);
        }
    }
}
