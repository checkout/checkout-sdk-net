using System.Net;
using System.Threading.Tasks;
using Checkout.Webhooks;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Webhooks
{
    public class RegisterWebhookTests : ApiTestFixture
    {
        [Fact]
        public async Task CanRegisterWebhook()
        {
            var webhook = TestHelper.CreateWebhook();

            var webhookRegistrationResponse = await Api.Webhooks.RegisterWebhookAsync(new RegisterWebhookRequest(webhook));

            webhookRegistrationResponse.ShouldNotBeNull();
            webhookRegistrationResponse.Id.ShouldStartWith("wh_");
            webhookRegistrationResponse.Url.ShouldBe(webhook.Url);
            webhookRegistrationResponse.EventTypes.ShouldBe(webhook.EventTypes);

            await Api.Webhooks.RemoveWebhookAsync(webhookRegistrationResponse.Id);
        }

        [Fact]
        public void GivenInvalidUrlShouldReturnValidationException()
        {
            var webhook = TestHelper.CreateWebhook();
            webhook.Url = "invalid";

            var checkoutValidationException = Should.Throw<CheckoutValidationException>(async () => await Api.Webhooks.RegisterWebhookAsync(new RegisterWebhookRequest(webhook)));

            checkoutValidationException.ShouldNotBeNull();
            checkoutValidationException.HttpStatusCode.ShouldBe((HttpStatusCode)422);
            checkoutValidationException.Error.ErrorCodes.ShouldContain("url_invalid");
        }
    }
}
