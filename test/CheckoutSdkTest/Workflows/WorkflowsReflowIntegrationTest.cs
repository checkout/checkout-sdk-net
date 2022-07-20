using Checkout.Payments.Response;
using Checkout.Workflows.Events;
using Checkout.Workflows.Reflows;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows
{
    public class WorkflowsReflowIntegrationTest : AbstractWorkflowIntegrationTest
    {
        [Fact(Skip = "unstable")]
        public async Task ShouldReflowByEvent()
        {
            await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            SubjectEvent paymentApprovedEvent = await Retriable(async () => await GetSubjectEvent(payment.Id));

            paymentApprovedEvent.ShouldNotBeNull();
            paymentApprovedEvent.Id.ShouldNotBeNullOrEmpty();

            ReflowResponse reflowResponse = await DefaultApi.WorkflowsClient().ReflowByEvent(paymentApprovedEvent.Id);

            reflowResponse.ShouldNotBeNull();
            reflowResponse.HttpStatusCode.ShouldNotBeNull();
            reflowResponse.ResponseHeaders.ShouldNotBeNull();
            
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldReflowBySubject()
        {
            await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            ReflowResponse reflowResponse =
                await Retriable(async () => await DefaultApi.WorkflowsClient().ReflowBySubject(payment.Id));

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldReflowByEventAndWorkflow()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            SubjectEvent paymentApprovedEvent = await Retriable(async () => await GetSubjectEvent(payment.Id));

            ReflowResponse reflowResponse = await Retriable(async () => await DefaultApi.WorkflowsClient()
                .ReflowByEventAndWorkflow(paymentApprovedEvent.Id, createWorkflowResponse.Id));

            reflowResponse.ShouldNotBeNull();
            reflowResponse.HttpStatusCode.ShouldNotBeNull();
            reflowResponse.ResponseHeaders.ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldReflowBySubjectAndWorkflow()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            ReflowResponse reflowResponse = await Retriable(async () => await DefaultApi.WorkflowsClient()
                .ReflowBySubjectAndWorkflow(payment.Id, createWorkflowResponse.Id));

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldReflowByEvents()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            SubjectEvent paymentApprovedEvent = await Retriable(async () => await GetSubjectEvent(payment.Id));

            ReflowByEventsRequest request = new ReflowByEventsRequest
            {
                Events = new List<string> {paymentApprovedEvent.Id},
                Workflows = new List<string> {createWorkflowResponse.Id},
            };

            ReflowResponse reflowResponse = await DefaultApi.WorkflowsClient().Reflow(request);

            reflowResponse.ShouldNotBeNull();
            reflowResponse.HttpStatusCode.ShouldNotBeNull();
            reflowResponse.ResponseHeaders.ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldReflowSubjects()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            ReflowBySubjectsRequest request = new ReflowBySubjectsRequest
            {
                Subjects = new List<string> {payment.Id}, Workflows = new List<string> {createWorkflowResponse.Id}
            };

            ReflowResponse reflowResponse =
                await Retriable(async () => await DefaultApi.WorkflowsClient().Reflow(request));

            reflowResponse.ShouldNotBeNull();
            reflowResponse.HttpStatusCode.ShouldNotBeNull();
            reflowResponse.ResponseHeaders.ShouldNotBeNull();
        }
    }
}