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
            var webhookRegistrationRequest = new WebhookRequest<RegistrationWebhook>(webhook.toRegistrationWebhook());

            var webhookRegistrationResponse = await Api.Webhooks.RegisterWebhookAsync(webhookRegistrationRequest);

            webhookRegistrationResponse.ShouldNotBeNull();

            var webhookRetrievalResponse = await Api.Webhooks.RetrieveWebhookAsync(webhookRegistrationResponse.Id);

            webhookRetrievalResponse.ShouldNotBeNull();
            webhookRetrievalResponse.Id.ShouldBe(webhookRegistrationResponse.Id);

            await Api.Webhooks.RemoveWebhookAsync(webhookRegistrationResponse.Id);
        }
    }
}
