using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Response;
using Checkout.Workflows.Actions;
using Checkout.Workflows.Actions.Request;
using Checkout.Workflows.Conditions.Request;
using Checkout.Workflows.Events;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Workflows
{
    public abstract class AbstractWorkflowIntegrationTest : SandboxTestFixture, IDisposable
    {
        private const string WorkflowEntityId = "ent_kidtcgc3ge5unf4a5i6enhnr5m";
        private static readonly string ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID");
        protected const string WorkflowName = "testing-net";

        private readonly HashSet<string> _workflows = new HashSet<string>();

        protected AbstractWorkflowIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        protected async Task<CreateWorkflowResponse> CreateWorkflow()
        {
            CreateWorkflowRequest createWorkflowRequest = new CreateWorkflowRequest
            {
                Actions =
                    new List<WorkflowActionRequest>
                    {
                        new WebhookWorkflowActionRequest
                        {
                            Url = "https://google.com/fail",
                            Headers = new Dictionary<string, string>(),
                            Signature = new WebhookSignature
                            {
                                Key = "8V8x0dLK%AyD*DNS8JJr", Method = "HMACSHA256"
                            }
                        }
                    },
                Conditions = new List<WorkflowConditionRequest>
                {
                    new EntityWorkflowConditionRequest {Entities = new List<string> {WorkflowEntityId}},
                    new EventWorkflowConditionRequest
                    {
                        Events = new Dictionary<string, ISet<string>>
                        {
                            {
                                "gateway",
                                new HashSet<string>
                                {
                                    "payment_approved",
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
                                    "payment_voided"
                                }
                            },
                            {
                                "dispute",
                                new HashSet<string>
                                {
                                    "dispute_canceled",
                                    "dispute_evidence_required",
                                    "dispute_expired",
                                    "dispute_lost",
                                    "dispute_resolved",
                                    "dispute_won"
                                }
                            }
                        }
                    },
                    new ProcessingChannelWorkflowConditionRequest
                    {
                        ProcessingChannels = new List<string> {ProcessingChannelId}
                    }
                },
                Name = "testing",
                Active = true
            };

            CreateWorkflowResponse response = await DefaultApi.WorkflowsClient().CreateWorkflow(createWorkflowRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            _workflows.Add(response.Id);
            return response;
        }

        protected async Task<PaymentResponse> MakeCardPayment(bool shouldCapture = false, long amount = 10L)
        {
            var phone = new Phone {CountryCode = "44", Number = "020 222333"};

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
                Phone = phone,
                Stored = false
            };

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = shouldCapture,
                Reference = Guid.NewGuid().ToString(),
                Amount = amount,
                Currency = Currency.GBP,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                Marketplace = new MarketplaceData {SubEntityId = "ent_ocw5i74vowfg2edpy66izhts2u"},
            };

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<SubjectEvent> GetSubjectEvent(string subjectId)
        {
            SubjectEventsResponse subjectEventsResponse = await DefaultApi.WorkflowsClient().GetSubjectEvents(subjectId);

            subjectEventsResponse.ShouldNotBeNull();
            subjectEventsResponse.Events.Count.ShouldBe(1);

            SubjectEvent paymentApprovedEvent =
                subjectEventsResponse.Events.FirstOrDefault(x => x.Type.Equals("payment_approved"));

            paymentApprovedEvent.ShouldNotBeNull();

            return paymentApprovedEvent;
        }

        public async void Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (string workflowId in _workflows)
            {
                await DefaultApi.WorkflowsClient().RemoveWorkflow(workflowId);
            }
        }
    }
}