using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Webhooks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Webhooks
{
    public class UpdateWebhookTests : ApiTestFixture
    {
        [Fact]
        public async Task CanUpdateWebhook()
        {
            var webhook = TestHelper.CreateWebhook();

            var webhookRegistrationResponse = await Api.Webhooks.RegisterWebhookAsync(new RegisterWebhookRequest(webhook));

            webhookRegistrationResponse.ShouldNotBeNull();

            webhookRegistrationResponse.Headers.TryGetValue("authorization", out string authorization);
            webhook.Url += "/updated";
            webhook.Active = !webhookRegistrationResponse.Active;
            webhook.EventTypes = new List<string>
            {
                "payment_refunded"
            };
            webhook.Headers.Add("Authorization", authorization);

            var webhookUpdateResponse = await Api.Webhooks.UpdateWebhookAsync(webhookRegistrationResponse.Id, new UpdateWebhookRequest(webhook));

            webhookUpdateResponse.ShouldNotBeNull();
            webhookUpdateResponse.Id.ShouldBe(webhookRegistrationResponse.Id);
            webhookUpdateResponse.Url.ShouldEndWith("/updated");
            webhookUpdateResponse.Active.ShouldNotBe(webhookRegistrationResponse.Active);
            webhookUpdateResponse.EventTypes.ShouldBe(webhook.EventTypes);

            await Api.Webhooks.RemoveWebhookAsync(webhookUpdateResponse.Id);
        }
    }
}
