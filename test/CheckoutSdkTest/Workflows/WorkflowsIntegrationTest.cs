﻿using Checkout.Workflows.Actions;
using Checkout.Workflows.Actions.Request;
using Checkout.Workflows.Actions.Response;
using Checkout.Workflows.Conditions;
using Checkout.Workflows.Conditions.Request;
using Checkout.Workflows.Conditions.Response;
using Checkout.Workflows.Events;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Checkout.Workflows
{
    public class WorkflowsIntegrationTest : AbstractWorkflowIntegrationTest
    {
        [Fact(Skip = "unstable")]
        public async Task ShouldCreateAndGetWorkflows()
        {
            var createdWorkflow = await CreateWorkflow();

            GetWorkflowResponse getWorkflowResponse =
                await DefaultApi.WorkflowsClient().GetWorkflow(createdWorkflow.Id);

            getWorkflowResponse.ShouldNotBeNull();
            getWorkflowResponse.Id.ShouldNotBeNullOrEmpty();
            getWorkflowResponse.Name.ShouldBe(WorkflowName);
            getWorkflowResponse.Active.ShouldBe(true);

            getWorkflowResponse.Actions.ShouldNotBeNull();
            getWorkflowResponse.Actions.Count.ShouldBe(1);
            getWorkflowResponse.Actions[0].ShouldBeOfType(typeof(WebhookWorkflowActionResponse));
            WebhookWorkflowActionResponse webhookWorkflowActionResponse =
                (WebhookWorkflowActionResponse)getWorkflowResponse.Actions[0];
            webhookWorkflowActionResponse.Headers.ShouldNotBeNull();
            webhookWorkflowActionResponse.Signature.ShouldNotBeNull();
            webhookWorkflowActionResponse.Url.ShouldNotBeNullOrEmpty();
            webhookWorkflowActionResponse.Id.ShouldNotBeNullOrEmpty();

            getWorkflowResponse.Conditions.ShouldNotBeNull();
            getWorkflowResponse.Conditions.Count.ShouldBe(3);
            foreach (WorkflowConditionResponse workflowConditionResponse in getWorkflowResponse.Conditions)
            {
                if (workflowConditionResponse is EntityWorkflowConditionResponse entityCondition)
                {
                    entityCondition.Type.ShouldBe(WorkflowConditionType.Entity);
                    entityCondition.Entities.ShouldNotBeEmpty();
                }
                else if (workflowConditionResponse is EventWorkflowConditionResponse eventCondition)
                {
                    eventCondition.Type.ShouldBe(WorkflowConditionType.Event);
                    eventCondition.Events.ShouldNotBeEmpty();
                }
                else if (workflowConditionResponse is ProcessingChannelWorkflowConditionResponse
                         processingChannelCondition)
                {
                    processingChannelCondition.Type.ShouldBe(WorkflowConditionType.ProcessingChannel);
                    processingChannelCondition.ProcessingChannels.ShouldNotBeEmpty();
                }
                else
                {
                    throw new XunitException("invalid workflow condition response");
                }
            }

            GetWorkflowsResponse getWorkflowsResponse = await DefaultApi.WorkflowsClient().GetWorkflows();
            getWorkflowsResponse.ShouldNotBeNull();
            getWorkflowsResponse.Workflows.ShouldNotBeEmpty();

            foreach (var workflow in getWorkflowsResponse.Workflows)
            {
                workflow.Name.ShouldBe(WorkflowName);
                workflow.Id.ShouldNotBeNullOrEmpty();
                workflow.GetLink("self").ShouldNotBeNull();
                workflow.Active.ShouldBe(true);
            }
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldCreateAndUpdateWorkflow()
        {
            var workflow = await CreateWorkflow();

            UpdateWorkflowRequest request = new UpdateWorkflowRequest { Name = "testing_2", Active = false };

            UpdateWorkflowResponse updateWorkflowResponse =
                await DefaultApi.WorkflowsClient().UpdateWorkflow(workflow.Id, request);

            updateWorkflowResponse.ShouldNotBeNull();
            updateWorkflowResponse.HttpStatusCode.ShouldNotBeNull();
            updateWorkflowResponse.ResponseHeaders.ShouldNotBeNull();
            updateWorkflowResponse.Body.ShouldNotBeNull();
            updateWorkflowResponse.Name.ShouldBe("testing_2");
            updateWorkflowResponse.Active.ShouldBe(false);
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldUpdateWorkflowAction()
        {
            var createdWorkflow = await CreateWorkflow();

            createdWorkflow.ShouldNotBeNull();
            createdWorkflow.Id.ShouldNotBeNull();

            GetWorkflowResponse getWorkflowResponse =
                await DefaultApi.WorkflowsClient().GetWorkflow(createdWorkflow.Id);

            getWorkflowResponse.ShouldNotBeNull();
            getWorkflowResponse.Id.ShouldNotBeNullOrEmpty();
            getWorkflowResponse.Name.ShouldBe(WorkflowName);
            getWorkflowResponse.Actions.ShouldNotBeNull();
            getWorkflowResponse.Actions.Count.ShouldBe(1);
            string actionId = getWorkflowResponse.Actions[0].Id;

            WebhookWorkflowActionRequest updateAction =
                new WebhookWorkflowActionRequest
                {
                    Url = "https://google.com/fail/fake",
                    Headers = new Dictionary<string, string>(),
                    Signature = new WebhookSignature { Key = "8V8x0dLK%AyD*DNS8JJr", Method = "HMACSHA256" }
                };

            var emptyResponse = await DefaultApi.WorkflowsClient()
                .UpdateWorkflowAction(getWorkflowResponse.Id, actionId, updateAction);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            GetWorkflowResponse getWorkflowResponseUpdated =
                await DefaultApi.WorkflowsClient().GetWorkflow(createdWorkflow.Id);
            getWorkflowResponseUpdated.Actions.ShouldNotBeNull();
            getWorkflowResponseUpdated.Actions.Count.ShouldBe(1);

            WorkflowActionResponse action = getWorkflowResponseUpdated.Actions[0];
            action.Id.ShouldNotBeNullOrEmpty();
            action.Type.ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldUpdateWorkflowCondition()
        {
            var createdWorkflow = await CreateWorkflow();

            createdWorkflow.ShouldNotBeNull();
            createdWorkflow.Id.ShouldNotBeNull();

            GetWorkflowResponse getWorkflowResponse =
                await DefaultApi.WorkflowsClient().GetWorkflow(createdWorkflow.Id);

            getWorkflowResponse.ShouldNotBeNull();
            getWorkflowResponse.Id.ShouldNotBeNullOrEmpty();
            getWorkflowResponse.Name.ShouldBe(WorkflowName);
            getWorkflowResponse.Conditions.ShouldNotBeNull();
            getWorkflowResponse.Conditions.Count.ShouldBe(3);

            WorkflowConditionResponse eventWorkflowConditionResponse =
                getWorkflowResponse.Conditions.FirstOrDefault(x => x.Type.Equals(WorkflowConditionType.Event));

            eventWorkflowConditionResponse.ShouldNotBeNull();

            EventWorkflowConditionRequest updateEventCondition = new EventWorkflowConditionRequest()
            {
                Events = new Dictionary<string, ISet<string>>
                {
                    {
                        "gateway",
                        new HashSet<string>
                        {
                            "card_verified",
                            "card_verification_declined",
                            "payment_approved",
                            "payment_pending",
                            "payment_declined",
                            "payment_voided",
                            "payment_captured",
                            "payment_refunded"
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
            };

            await DefaultApi.WorkflowsClient().UpdateWorkflowCondition(getWorkflowResponse.Id,
                eventWorkflowConditionResponse.Id, updateEventCondition);

            GetWorkflowResponse getWorkflowResponse2 =
                await DefaultApi.WorkflowsClient().GetWorkflow(createdWorkflow.Id);

            getWorkflowResponse2.ShouldNotBeNull();
            getWorkflowResponse2.Conditions.ShouldNotBeNull();
            getWorkflowResponse2.Conditions.Count.ShouldBe(3);

            WorkflowConditionResponse updatedEventConditionResponse =
                getWorkflowResponse2.Conditions.FirstOrDefault(x => x.Type.Equals(WorkflowConditionType.Event));

            updatedEventConditionResponse.ShouldNotBeNull();
            updatedEventConditionResponse.Id.ShouldNotBeNull();
            updatedEventConditionResponse.Type.ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        public async Task ShouldCreateAndTestWorkflows()
        {
            var createdWorkflow = await CreateWorkflow();

            var eventTypesRequest = new EventTypesRequest
            {
                EventTypes = new List<string>
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
                    "payment_voided",
                    "dispute_canceled",
                    "dispute_evidence_required",
                    "dispute_expired",
                    "dispute_lost",
                    "dispute_resolved",
                    "dispute_won"
                }
            };

            EmptyResponse getWorkflowResponse =
                await DefaultApi.WorkflowsClient().TestWorkflow(createdWorkflow.Id, eventTypesRequest);

            getWorkflowResponse.ShouldNotBeNull();
        }
    }
}