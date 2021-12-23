using Checkout.Payments.Four.Response;
using Checkout.Workflows.Four.Events;
using Checkout.Workflows.Four.Reflows;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowReflowTest : AbstractWorkflowTest, IDisposable
    {
        [Fact(Timeout = 180000, Skip = "There is no event type Payment Approved")]
        public async Task ShouldReflowByEvent()
        {
            await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment(false);

            await Nap();

            SubjectEvent paymentApprovedEvent = await GetSubjectEvent(payment.Id);

            paymentApprovedEvent.ShouldNotBeNull();
            paymentApprovedEvent.Id.ShouldNotBeNullOrEmpty();

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().ReflowByEvent(paymentApprovedEvent.Id);

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Timeout = 180000, Skip = "There is no-reflow by payment Id")]
        public async Task ShouldReflowBySubject()
        {
            await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment(false);

            await Nap();

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().ReflowBySubject(payment.Id);

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Timeout = 180000, Skip = "There is no event type Payment Approved")]
        public async Task ShouldReflowByEventAndWorkflow()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment(false);

            await Nap();

            SubjectEvent paymentApprovedEvent = await GetSubjectEvent(payment.Id);

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().ReflowByEventAndWorkflow(paymentApprovedEvent.Id, createWorkflowResponse.Id);

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Timeout = 180000, Skip = "There is no-reflow by payment Id and workflow Id")]
        public async Task ShouldReflowBySubjectAndWorkflow()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment(false);

            await Nap();

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().ReflowBySubjectAndWorkflow(payment.Id, createWorkflowResponse.Id);

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Timeout = 180000, Skip = "There is no event type Payment Approved")]
        public async Task ShouldReflowByEvents()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment(false);

            await Nap();

            SubjectEvent paymentApprovedEvent = await GetSubjectEvent(payment.Id);

            ReflowByEventsRequest request = new ReflowByEventsRequest(
                new List<string>() { paymentApprovedEvent.Id },
                new List<string>() { createWorkflowResponse.Id }
                );

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().Reflow(request);

            reflowResponse.ShouldNotBeNull();
        }

        [Fact(Timeout = 180000, Skip = "There is no-reflow by payment id and workflow id")]
        public async Task ShouldReflowSubjects()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            PaymentResponse payment = await MakeCardPayment(false);

            await Nap();

            ReflowBySubjectsRequest request = new ReflowBySubjectsRequest(
                new List<string>() { payment.Id },
                new List<string>() { createWorkflowResponse.Id }
                );

            ReflowResponse reflowResponse = await FourApi.WorkflowsClient().Reflow(request);

            reflowResponse.ShouldNotBeNull();
        }

        public void Dispose()
        {
            CleanupWorkflow();
        }
    }
}