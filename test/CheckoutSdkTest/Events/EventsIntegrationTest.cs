using Checkout.Payments;
using Checkout.Webhooks;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Events
{
    public class EventsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        private readonly List<string> _paymentEventTypes = new List<string>
        {
            "payment_approved",
            "payment_canceled",
            "payment_capture_declined",
            "payment_capture_pending",
            "payment_captured",
            "payment_chargeback",
            "payment_declined",
            "payment_expired",
            "payment_paid",
            "payment_pending",
            "payment_refund_declined",
            "payment_refund_pending",
            "payment_refunded",
            "payment_retrieval",
            "payment_void_declined",
            "payment_voided",
        };

        [Fact]
        private async Task ShouldRetrieveDefaultEventTypes()
        {
            var allEventTypesWrapper = await DefaultApi.EventsClient().RetrieveAllEventTypes();
            var allEventTypes = allEventTypesWrapper.Items;
            allEventTypes.ShouldNotBeNull();
            allEventTypes.Count.ShouldBe(2);
            allEventTypes[0].EventTypes.ShouldNotBeNull();

            var versionOneEventTypesWrap = await DefaultApi.EventsClient().RetrieveAllEventTypes(allEventTypes[0].Version);
            var versionOneEventTypes = versionOneEventTypesWrap.Items;
            versionOneEventTypes.ShouldNotBeNull();
            versionOneEventTypes.Count.ShouldBe(1);
            versionOneEventTypes[0].Version.ShouldBe(allEventTypes[0].Version);
            versionOneEventTypes[0].EventTypes.Count.ShouldBe(allEventTypes[0].EventTypes.Count);

            var versionTwoEventTypesWrap = await DefaultApi.EventsClient().RetrieveAllEventTypes(allEventTypes[1].Version);
            var versionTwoEventTypes = versionTwoEventTypesWrap.Items;
            versionTwoEventTypes.ShouldNotBeNull();
            versionTwoEventTypes.Count.ShouldBe(1);
            versionTwoEventTypes[0].Version.ShouldBe(allEventTypes[1].Version);
            versionTwoEventTypes[0].EventTypes.Count.ShouldBe(allEventTypes[1].EventTypes.Count);
        }

        [Fact(Timeout = 180000, Skip = "Due the time to expect the dispute, just run as needed")]
        private async Task ShouldRetrieveEventsByPaymentId_andRetrieveEventById_andGetNotification()
        {
            var webhook = await RegisterWebhook();

            var payment = await MakeCardPayment();
            payment.ShouldNotBeNull();


            var retrieveEventsRequest = new RetrieveEventsRequest {PaymentId = payment.Id};

            //Retrieve events
            EventsPageResponse events = await Retriable(async () =>
                await DefaultApi.EventsClient().RetrieveEvents(retrieveEventsRequest));

            events.ShouldNotBeNull();
            events.TotalCount.ShouldBe(1);
            events.Data.ShouldNotBeNull();
            events.Data.Count.ShouldBe(1);

            var summaryResponse = events.Data[0];
            summaryResponse.Id.ShouldNotBeNull();
            summaryResponse.CreatedOn.ShouldNotBeNull();
            summaryResponse.Type.ShouldBe("payment_approved");

            //Retrieve event
            var eventResponse = await DefaultApi.EventsClient().RetrieveEvent(summaryResponse.Id);
            eventResponse.ShouldNotBeNull();
            eventResponse.Id.ShouldNotBeNull();
            eventResponse.Data.ShouldNotBeNull();
            eventResponse.Type.ShouldBe("payment_approved");

            //Get notification
            var eventNotification = await DefaultApi.EventsClient()
                .RetrieveEventNotification(eventResponse.Id, eventResponse.Notifications[0].Id);

            eventNotification.ShouldNotBeNull();
            eventNotification.Id.ShouldNotBeNull();
            eventNotification.Url.ShouldNotBeNull();
            eventNotification.Success.ShouldNotBeNull();

            // Retry Webhooks
            // Webhooks are not being re attempted. Adding the call to ensure.
            await DefaultApi.EventsClient().RetryWebhook(summaryResponse.Id, webhook.Id);

            await DefaultApi.EventsClient().RetryAllWebhooks(summaryResponse.Id);
        }


        private async Task<WebhookResponse> RegisterWebhook()
        {
            var webhookRequest = new WebhookRequest()
            {
                Url = "https://checkout.com/webhooks",
                ContentType = WebhookContentType.Json,
                EventTypes = _paymentEventTypes
            };
            return await DefaultApi.WebhooksClient().RegisterWebhook(webhookRequest);
        }
    }
}