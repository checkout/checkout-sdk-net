using System.Threading.Tasks;
using Checkout.Webhooks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Webhooks
{
    public class RetrieveWebhookTests : ApiTestFixture
    {
        [Fact]
        public async Task CanRetrieveWebhook()
        {
            var webhook = TestHelper.CreateWebhook();

            var webhookRegistrationResponse = await Api.Webhooks.RegisterWebhookAsync(new RegisterWebhookRequest(webhook));

            webhookRegistrationResponse.ShouldNotBeNull();

            var webhookRetrievalResponse = await Api.Webhooks.RetrieveWebhookAsync(webhookRegistrationResponse.Id);

            webhookRetrievalResponse.ShouldNotBeNull();
            webhookRetrievalResponse.Id.ShouldBe(webhookRegistrationResponse.Id);

            await Api.Webhooks.RemoveWebhookAsync(webhookRegistrationResponse.Id);
        }
    }
}
