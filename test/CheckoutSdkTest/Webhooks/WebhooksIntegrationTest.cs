using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Shouldly;
using Xunit;

namespace Checkout.Webhooks
{
    public class WebhooksIntegrationTest : SandboxTestFixture
    {
        private readonly List<string> _eventTypes = new List<string>
        {
            "invoice.cancelled",
            "card.updated",
        };

        public WebhooksIntegrationTest() : base(PlatformType.Default)
        {
            Task.Run(CleanUp);
        }

        private async Task CleanUp()
        {
            var webhookResponses = await DefaultApi.WebhooksClient().RetrieveWebhooks();
            if (!webhookResponses.IsNullOrEmpty())
            {
                foreach (var webhook in webhookResponses)
                {
                    webhook.ShouldNotBeNull();
                    await DefaultApi.WebhooksClient().RemoveWebhook(webhook.Id);
                }
            }
        }

        [Fact(Timeout = 12000, Skip = "Commented because an error (404)")]
        private async Task ShouldTestFullWebhookOperations()
        {
            const string url = "https://checkout.com/webhooks";
            //Create webhook
            var webhookResponse = await RegisterWebhook(url);
            webhookResponse.ShouldNotBeNull();
            webhookResponse.Id.ShouldNotBeNull();
            webhookResponse.Url.ShouldBe(url);
            webhookResponse.ContentType.ShouldBe(WebhookContentType.Json);
            webhookResponse.EventTypes.ShouldBe(_eventTypes);

            await Nap();

            //Retrieve webhook
            var retrieveWebhook = await DefaultApi.WebhooksClient().RetrieveWebhook(webhookResponse.Id);
            retrieveWebhook.ShouldNotBeNull();
            retrieveWebhook.Id.ShouldBe(webhookResponse.Id);
            retrieveWebhook.Url.ShouldBe(webhookResponse.Url);
            retrieveWebhook.Active.ShouldBe(webhookResponse.Active);
            retrieveWebhook.ContentType.ShouldBe(webhookResponse.ContentType);
            retrieveWebhook.EventTypes.ShouldBe(webhookResponse.EventTypes);

            //Update webhook
            const string urlChanged = "https://checkout.com/failed2";
            var updateRequest = new WebhookRequest()
            {
                Url = urlChanged,
                Headers = retrieveWebhook.Headers,
                EventTypes = new List<string> {"source_updated"},
                ContentType = WebhookContentType.Json
            };

            await Nap();

            var updateWebhook = await DefaultApi.WebhooksClient().UpdateWebhook(webhookResponse.Id, updateRequest);
            updateWebhook.ShouldNotBeNull();
            updateWebhook.Url.ShouldBe(urlChanged);
            updateWebhook.Headers.ShouldBe(retrieveWebhook.Headers);
            updateWebhook.EventTypes.ShouldNotBe(retrieveWebhook.EventTypes);
            updateWebhook.EventTypes.ShouldBe(updateRequest.EventTypes);

            //Delete webhook
            await DefaultApi.WebhooksClient().RemoveWebhook(webhookResponse.Id);

            //Confirm delete
            try
            {
                var nonExistingWebhook = await DefaultApi.WebhooksClient().RetrieveWebhook(webhookResponse.Id);
                nonExistingWebhook.ShouldBeNull();
            }
            catch (CheckoutApiException ex)
            {
                ex.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }

        private async Task<WebhookResponse> RegisterWebhook(string url)
        {
            var webhookRequest = new WebhookRequest()
            {
                Url = url,
                ContentType = WebhookContentType.Json,
                EventTypes = _eventTypes
            };
            return await DefaultApi.WebhooksClient().RegisterWebhook(webhookRequest);
        }
    }
}