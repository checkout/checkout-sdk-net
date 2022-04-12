using Checkout.Payments.Four.Response;
using Checkout.Workflows.Four.Actions;
using Checkout.Workflows.Four.Actions.Response;
using Checkout.Workflows.Four.Events;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowActionsIntegrationTest : AbstractWorkflowIntegrationTest
    {
        [Fact(Skip = "unstable")]
        public async Task ShouldGetActionInvocations()
        {
            var createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            SubjectEventsResponse subjectEventsResponse =
                await Retriable(async () => await FourApi.WorkflowsClient().GetSubjectEvents(payment.Id));
            subjectEventsResponse.ShouldNotBeNull();
            subjectEventsResponse.Events.ShouldNotBeEmpty();
            subjectEventsResponse.Events[0].Id.ShouldNotBeNull();

            var getWorkflowResponseUpdated =
                await FourApi.WorkflowsClient().GetWorkflow(createWorkflowResponse.Id);
            getWorkflowResponseUpdated.ShouldNotBeNull();
            getWorkflowResponseUpdated.Actions.ShouldNotBeEmpty();

            var actionId = getWorkflowResponseUpdated.Actions[0].Id;
            var eventId = subjectEventsResponse.Events[0].Id;

            var actionInvocations =
                await FourApi.WorkflowsClient().GetActionInvocations(eventId, actionId);

            actionInvocations.ShouldNotBeNull();
            actionInvocations.Status.ShouldBe(WorkflowActionStatus.Pending);
            actionInvocations.ActionType.ShouldBe(WorkflowActionType.Webhook);
            actionInvocations.EventId.ShouldBe(eventId);
            actionInvocations.WorkflowId.ShouldBe(createWorkflowResponse.Id);
            actionInvocations.WorkflowActionId.ShouldBe(actionId);
            actionInvocations.ActionInvocations.ShouldNotBeNull();
            actionInvocations.ActionInvocations.ShouldNotBeEmpty();

            foreach (WorkflowActionInvocation actionInvocationsActionInvocation in actionInvocations.ActionInvocations)
            {
                actionInvocationsActionInvocation.Final.ShouldNotBeNull();
                actionInvocationsActionInvocation.Retry.ShouldNotBeNull();
                actionInvocationsActionInvocation.Succeeded.ShouldNotBeNull();
                actionInvocationsActionInvocation.Timestamp.ShouldNotBeNull();
                actionInvocationsActionInvocation.InvocationId.ShouldNotBeNullOrEmpty();
                actionInvocationsActionInvocation.Result.ShouldNotBeEmpty();
            }
        }
    }
}