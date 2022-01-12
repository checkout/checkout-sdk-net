using Checkout.Payments.Four.Response;
using Checkout.Workflows.Four.Events;
using Checkout.Workflows.Four.Reflows;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowsReflowIntegrationTest : AbstractWorkflowIntegrationTest
    {
        [Fact(Timeout = 180000)]
        public async Task ShouldReflowByEvent()
        {
            await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            await Nap(10);

            SubjectEvent paymentApprovedEvent = await GetSubjectEvent(payment.Id);

            paymentApprovedEvent.ShouldNotBeNull();
            paymentApprovedEvent.Id.ShouldNotBeNullOrEmpty();

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().ReflowByEvent(paymentApprovedEvent.Id);

            reflowResponse.ShouldBeNull();
        }

        [Fact(Timeout = 180000, Skip = "unstable")]
        public async Task ShouldReflowBySubject()
        {
            await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            await Nap(10);

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().ReflowBySubject(payment.Id);

            reflowResponse.ShouldBeNull();
        }

        [Fact(Timeout = 180000)]
        public async Task ShouldReflowByEventAndWorkflow()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            await Nap(10);

            SubjectEvent paymentApprovedEvent = await GetSubjectEvent(payment.Id);

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient()
                .ReflowByEventAndWorkflow(paymentApprovedEvent.Id, createWorkflowResponse.Id);

            reflowResponse.ShouldBeNull();
        }

        [Fact(Timeout = 180000)]
        public async Task ShouldReflowBySubjectAndWorkflow()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            await Nap(10);

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient()
                .ReflowBySubjectAndWorkflow(payment.Id, createWorkflowResponse.Id);

            reflowResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldReflowByEvents()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            await Nap(10);

            SubjectEvent paymentApprovedEvent = await GetSubjectEvent(payment.Id);

            ReflowByEventsRequest request = new ReflowByEventsRequest
            {
                Events = new List<string> {paymentApprovedEvent.Id},
                Workflows = new List<string> {createWorkflowResponse.Id},
            };

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().Reflow(request);

            reflowResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldReflowSubjects()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment();

            await Nap(10);

            ReflowBySubjectsRequest request = new ReflowBySubjectsRequest
            {
                Subjects = new List<string> {payment.Id}, Workflows = new List<string> {createWorkflowResponse.Id}
            };

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().Reflow(request);

            reflowResponse.ShouldBeNull();
        }
    }
}