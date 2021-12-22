using Checkout.Payments.Four.Response;
using Checkout.Workflows.Four.Events;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowEventsTest : AbstractWorkflowTest, IDisposable
    {
        public void Dispose()
        {
            CleanupWorkflow();
        }

        [Fact]
        public async Task ShouldEventTypes()
        {
            List<EventTypesResponse> eventTypesResposes
                = (List<EventTypesResponse>)await FourApi.WorkflowsClient().GetEventTypes();

            eventTypesResposes.ShouldNotBeNull();
            eventTypesResposes.Count.ShouldBe(7);

            foreach (var eventType in eventTypesResposes)
            {
                eventType.Id.ShouldNotBeNullOrEmpty();
                eventType.Description.ShouldNotBeNullOrEmpty();
                eventType.DisplayName.ShouldNotBeNullOrEmpty();

                foreach (var ev in eventType.Events)
                {
                    ev.Description.ShouldNotBeNullOrEmpty();
                    ev.DisplayName.ShouldNotBeNullOrEmpty();
                    ev.Id.ShouldNotBeNullOrEmpty();
                }
            }
        }

        [Fact(Timeout = 180000, Skip = "There is no payment by payment id and capture request")]
        public async Task ShouldGetSubjectEventAndEvents()
        {
            await CreateWorkflow();

            PaymentResponse paymentResponse = await MakeCardPayment();

            await Nap();

            await CaptureResponse(paymentResponse.Id);

            await Nap();

            SubjectEventsResponse subjectEventsResponse = await FourApi.WorkflowsClient().GetSubjectEvents(paymentResponse.Id);

            subjectEventsResponse.ShouldNotBeNull();
            subjectEventsResponse.Events.Count.ShouldBe(2);

            SubjectEvent paymentApprovedEvent = subjectEventsResponse.Events.FirstOrDefault(x => x.Type.Equals("payment_approved"));

            paymentApprovedEvent.ShouldNotBeNull();
            paymentApprovedEvent.Id.ShouldNotBeNullOrEmpty();
            paymentApprovedEvent.Timestamp.ShouldNotBeNullOrEmpty();
            paymentApprovedEvent.GetLink("self").ShouldNotBeNull();

            SubjectEvent paymentCapturedEvent = subjectEventsResponse.Events.FirstOrDefault(x => x.Type.Equals("payment_captured"));

            paymentCapturedEvent.ShouldNotBeNull();
            paymentCapturedEvent.Id.ShouldNotBeNullOrEmpty();
            paymentCapturedEvent.Timestamp.ShouldNotBeNullOrEmpty();
            paymentCapturedEvent.GetLink("self").ShouldNotBeNull();

            GetEventResponse getEventResponse = await FourApi.WorkflowsClient().GetEvent(paymentCapturedEvent.Id);

            getEventResponse.ShouldNotBeNull();
            getEventResponse.Id.ShouldNotBeNull();
            getEventResponse.Timestamp.ShouldNotBeNull();
            getEventResponse.Version.ShouldNotBeNull();
            getEventResponse.Data.ShouldNotBeNull();
        }
    }
}