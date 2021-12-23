using Checkout.Common;
using Checkout.Payments.Four;
using Checkout.Payments.Four.Request;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Response;
using Checkout.Payments.Four.Sender;
using Checkout.Workflows.Four.Actions.Request;
using Checkout.Workflows.Four.Conditions.Request;
using Checkout.Workflows.Four.Events;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Workflows.Four
{
    public abstract class AbstractWorkflowTest : SandboxTestFixture
    {
        private const string WORKFLOW_ENTITY_ID = "ent_kidtcgc3ge5unf4a5i6enhnr5m";
        private HashSet<string> workflows = new HashSet<string>();

        protected readonly string conditionsGateway = @"payment_approved,card_verification_declined,card_verified,
                                             payment_authorization_incremented,payment_authorization_increment_declined,
                                             payment_capture_declined,payment_captured,payment_refund_declined,payment_refunded,
                                             payment_void_declined,payment_voided";

        protected readonly string conditionsDispute = @"dispute_canceled,dispute_evidence_required,dispute_expired,dispute_lost,
                                                    dispute_resolved,dispute_won";

        protected readonly string nameWorkFlow = "testing";

        protected AbstractWorkflowTest() : base(PlatformType.FourOAuth)
        {
        }

        protected async Task<CreateWorkflowResponse> CreateWorkflow()
        {
            CreateWorkflowRequest createWorkflowRequest = new CreateWorkflowRequest()
            {
                Actions = new List<WorkflowActionRequest>()
                {
                    new WebhookWorkflowActionRequest("https://google.com/fail",new Dictionary<string,string>(), new Actions.WebhookSignature() { Key="8V8x0dLK%AyD*DNS8JJr", Method="HMACSHA256"})
                },
                Conditions = new List<WorkflowConditionRequest>()
                {
                    new EntityWorkflowConditionRequest( new List<string>() { WORKFLOW_ENTITY_ID }),
                    new EventWorkflowConditionRequest( new Dictionary<string, ISet<string>>() {
                        {"gateway", new HashSet<string>{"payment_approved",
                                                        "payment_declined",
                                                        "card_verification_declined",
                                                        "card_verified",
                                                        "payment_authorization_incremented",
                                                        "payment_authorization_increment_declined",
                                                        "payment_capture_declined",
                                                        "payment_captured",
                                                        "payment_refund_declined",
                                                        "payment_refunded",
                                                        "payment_void_declined",
                                                        "payment_voided"} },
                        {"dispute", new HashSet<string>{"dispute_canceled",
                                                        "dispute_evidence_required",
                                                        "dispute_expired",
                                                        "dispute_lost",
                                                        "dispute_resolved",
                                                        "dispute_won" } }
                    })
                },
                Name = "testing"
            };

            CreateWorkflowResponse response = await FourApi.WorkflowsClient().CreateWorkflow(createWorkflowRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();

            QueueWorkflowCleanup(response.Id);

            return response;
        }

        protected async Task<PaymentResponse> MakeCardPayment(bool shouldCapture = false, long amount = 10L)
        {
            var phone = new Phone
            {
                CountryCode = "44",
                Number = "020 222333"
            };

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var customerRequest = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer"
            };

            var address = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var paymentIndividualSender = new PaymentIndividualSender
            {
                FirstName = "Mr",
                LastName = "Test",
                Address = address
            };

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = shouldCapture,
                Reference = Guid.NewGuid().ToString(),
                Amount = amount,
                Currency = Currency.USD,
                Customer = customerRequest,
                Sender = paymentIndividualSender,
                ProcessingChannelId = "pc_a6dabcfa2o3ejghb3sjuotdzzy",
                Marketplace = new MarketplaceData
                {
                    SubEntityId = "ent_ocw5i74vowfg2edpy66izhts2u"
                },
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<CaptureResponse> CaptureResponse(string paymentId)
        {
            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString(),
                Amount = 10
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await FourApi.PaymentsClient().CapturePayment(paymentId, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            return response;
        }

        protected async Task<SubjectEvent> GetSubjectEvent(string subjectId)
        {
            SubjectEventsResponse subjectEventsResponse = await FourApi.WorkflowsClient().GetSubjectEvents(subjectId);

            subjectEventsResponse.ShouldNotBeNull();
            subjectEventsResponse.Events.Count.ShouldBe(1);

            SubjectEvent paymentApprovedEvent = subjectEventsResponse.Events.FirstOrDefault(x => x.Type.Equals("payment_approved"));

            paymentApprovedEvent.ShouldNotBeNull();

            return paymentApprovedEvent;
        }

        private void QueueWorkflowCleanup(string workflowid)
        {
            this.workflows.Add(workflowid);
        }

        public async void CleanupWorkflow()
        {
            foreach (var workflowsId in workflows)
            {
                await FourApi.WorkflowsClient().RemoveWorkflow(workflowsId);
            }
        }
    }
}