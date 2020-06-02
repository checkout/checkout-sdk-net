using System.Net;
using System.Threading.Tasks;
using Checkout.Webhooks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Webhooks
{
    public class RemoveWebhookTests : ApiTestFixture
    {
        [Fact]
        public async Task CanRemoveWebhook()
        {
            var webhook = TestHelper.CreateWebhook();
            var webhookRegistrationRequest = new WebhookRequest<RegistrationWebhook>(webhook.toRegistrationWebhook());

            var webhookRegistrationResponse = await Api.Webhooks.RegisterWebhookAsync(webhookRegistrationRequest);

            webhookRegistrationResponse.ShouldNotBeNull();

            await Api.Webhooks.RemoveWebhookAsync(webhookRegistrationResponse.Id);

            var webhookRetrievalResponse = Should.Throw<CheckoutResourceNotFoundException>(async () => await Api.Webhooks.RetrieveWebhookAsync(webhookRegistrationResponse.Id));

            webhookRetrievalResponse.ShouldNotBeNull();
            webhookRetrievalResponse.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
