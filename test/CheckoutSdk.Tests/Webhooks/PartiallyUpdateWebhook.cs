using System.Threading.Tasks;
using Checkout.Webhooks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Webhooks
{
    public class PartiallyUpdateWebhookTests : ApiTestFixture
    {
        [Fact]
        public async Task CanPartiallyUpdateWebhook()
        {
            var webhook = TestHelper.CreateWebhook();

            var webhookRegistrationResponse = await Api.Webhooks.RegisterWebhookAsync(new RegisterWebhookRequest(webhook));

            webhookRegistrationResponse.ShouldNotBeNull();

            webhook.Url += "/partially/updated";
            webhook.Headers = null;
            webhook.EventTypes = null;

            var webhookPartialUpdateResponse = await Api.Webhooks.PartiallyUpdateWebhookAsync(webhookRegistrationResponse.Id, new PartialUpdateWebhookRequest(webhook));

            webhookPartialUpdateResponse.ShouldNotBeNull();
            webhookPartialUpdateResponse.Id.ShouldBe(webhookRegistrationResponse.Id);
            webhookPartialUpdateResponse.Url.ShouldEndWith("/partially/updated");
            webhookPartialUpdateResponse.Active.ShouldBe(webhookRegistrationResponse.Active);
            webhookPartialUpdateResponse.Headers.ShouldBe(webhookRegistrationResponse.Headers);
            webhookPartialUpdateResponse.EventTypes.ShouldBe(webhookRegistrationResponse.EventTypes);

            await Api.Webhooks.RemoveWebhookAsync(webhookPartialUpdateResponse.Id);
        }
    }
}
