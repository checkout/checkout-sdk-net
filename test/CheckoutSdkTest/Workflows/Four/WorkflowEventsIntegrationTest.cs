using Checkout.Payments.Four.Response;
using Checkout.Workflows.Four.Events;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowEventsIntegrationTest : AbstractWorkflowIntegrationTest
    {
        [Fact]
        public async Task ShouldGetEventTypes()
        {
            ItemsResponse<EventTypesResponse> eventTypesResponsesWrapper = await FourApi.WorkflowsClient().GetEventTypes();

            var eventTypesResponses = eventTypesResponsesWrapper.Items;
            eventTypesResponses.ShouldNotBeNull();
            eventTypesResponses.Count.ShouldBe(9);

            foreach (var eventType in eventTypesResponses)
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

        [Fact(Skip = "unstable")]
        public async Task ShouldGetSubjectEventAndEvents()
        {
            await CreateWorkflow();

            PaymentResponse paymentResponse = await MakeCardPayment(true);

            SubjectEventsResponse subjectEventsResponse =
                await Retriable(async () => await FourApi.WorkflowsClient().GetSubjectEvents(paymentResponse.Id),
                    HasTwoEvents);

            subjectEventsResponse.ShouldNotBeNull();
            subjectEventsResponse.Events.Count.ShouldBe(2);

            SubjectEvent paymentApprovedEvent =
                subjectEventsResponse.Events.FirstOrDefault(x => x.Type.Equals("payment_approved"));

            paymentApprovedEvent.ShouldNotBeNull();
            paymentApprovedEvent.Id.ShouldNotBeNullOrEmpty();
            paymentApprovedEvent.Timestamp.ShouldNotBeNullOrEmpty();
            paymentApprovedEvent.GetLink("self").ShouldNotBeNull();

            SubjectEvent paymentCapturedEvent =
                subjectEventsResponse.Events.FirstOrDefault(x => x.Type.Equals("payment_captured"));

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

        private static bool HasTwoEvents(SubjectEventsResponse obj)
        {
            return obj.Events.Count == 2;
        }
    }
}