using Castle.Core.Internal;
using Shouldly;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Webhooks.Previous
{
    public class WebhooksIntegrationTest : SandboxTestFixture
    {
        private readonly List<string> _eventTypes = new List<string> {"payment_approved", "payment_pending",
            "payment_declined", "payment_expired", "payment_canceled", "payment_voided", "payment_void_declined",
            "payment_captured", "payment_capture_declined", "payment_capture_pending", "payment_refunded",
            "payment_refund_declined", "payment_refund_pending"};

        public WebhooksIntegrationTest() : base(PlatformType.Previous)
        {
        }

        private async Task CleanUp()
        {
            var webhookResponses = await PreviousApi.WebhooksClient().RetrieveWebhooks();
            webhookResponses.HttpStatusCode.ShouldNotBeNull();
            webhookResponses.HttpStatusCode.ShouldNotBeNull();
            if (!webhookResponses.Items.IsNullOrEmpty())
            {
                foreach (var webhook in webhookResponses.Items)
                {
                    webhook.ShouldNotBeNull();
                    await PreviousApi.WebhooksClient().RemoveWebhook(webhook.Id);
                }
            }
        }

        [Fact(Skip = "unstable")]
        private async Task ShouldTestFullWebhookOperations()
        {
            await CleanUp();
            const string url = "https://checkout.com/webhooks";
            //Create webhook
            var webhookResponse = await RegisterWebhook(url);
            webhookResponse.ShouldNotBeNull();
            webhookResponse.Id.ShouldNotBeNull();
            webhookResponse.Url.ShouldBe(url);
            webhookResponse.ContentType.ShouldBe(WebhookContentType.Json);
            webhookResponse.EventTypes.ShouldBe(_eventTypes);

            //Retrieve webhook
            WebhookResponse retrieveWebhook = await Retriable(async () =>
                await PreviousApi.WebhooksClient().RetrieveWebhook(webhookResponse.Id));

            retrieveWebhook.ShouldNotBeNull();
            retrieveWebhook.Id.ShouldBe(webhookResponse.Id);
            retrieveWebhook.Url.ShouldBe(webhookResponse.Url);
            retrieveWebhook.Active.ShouldBe(webhookResponse.Active);
            retrieveWebhook.ContentType.ShouldBe(webhookResponse.ContentType);
            retrieveWebhook.EventTypes.ShouldBe(webhookResponse.EventTypes);

            //Update webhook
            const string urlChanged = "https://checkout.com/failed2";
            var updateRequest = new WebhookRequest
            {
                Url = urlChanged,
                Headers = retrieveWebhook.Headers,
                EventTypes = new List<string> {"source_updated"},
                ContentType = WebhookContentType.Json
            };

            var updateWebhook = await Retriable(async () =>
                await PreviousApi.WebhooksClient().UpdateWebhook(webhookResponse.Id, updateRequest));

            updateWebhook.ShouldNotBeNull();
            updateWebhook.HttpStatusCode.ShouldNotBeNull();
            updateWebhook.ResponseHeaders.ShouldNotBeNull();
            updateWebhook.Url.ShouldBe(urlChanged);
            updateWebhook.Headers.ShouldBe(retrieveWebhook.Headers);
            updateWebhook.EventTypes.ShouldNotBe(retrieveWebhook.EventTypes);
            updateWebhook.EventTypes.ShouldBe(updateRequest.EventTypes);

            //Delete webhook
            var emptyResponse = await PreviousApi.WebhooksClient().RemoveWebhook(webhookResponse.Id);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            //Confirm delete
            try
            {
                var nonExistingWebhook = await PreviousApi.WebhooksClient().RetrieveWebhook(webhookResponse.Id);
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
                Url = url, ContentType = WebhookContentType.Json, EventTypes = _eventTypes
            };
            return await PreviousApi.WebhooksClient().RegisterWebhook(webhookRequest);
        }
    }
}