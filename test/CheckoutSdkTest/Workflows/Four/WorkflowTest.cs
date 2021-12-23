using Checkout.Workflows.Four.Actions.Request;
using Checkout.Workflows.Four.Actions.Response;
using Checkout.Workflows.Four.Conditions;
using Checkout.Workflows.Four.Conditions.Request;
using Checkout.Workflows.Four.Conditions.Response;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowTest : AbstractWorkflowTest
    {
        [Fact(Timeout = 180000, Skip = " ")]
        public async Task ShouldCreateAndGetWorkflows()
        {
            var createdWorkFlow = await CreateWorkflow();

            createdWorkFlow.ShouldNotBeNull();
            createdWorkFlow.Id.ShouldNotBeNull();

            GetWorkflowResponse getWorkflowResponse = await FourApi.WorkflowsClient().GetWorkflow(createdWorkFlow.Id);

            getWorkflowResponse.ShouldNotBeNull();
            getWorkflowResponse.Id.ShouldNotBeNullOrEmpty();
            Assert.Equal(nameWorkFlow, getWorkflowResponse.Name);
            getWorkflowResponse.Actions.ShouldNotBeNull();
            getWorkflowResponse.Actions.Count.ShouldBe(1);
            Assert.True(getWorkflowResponse.Actions.FirstOrDefault() is WebhookWorkflowActionResponse);

            WebhookWorkflowActionResponse webhookWorkflowActionResponse = (WebhookWorkflowActionResponse)getWorkflowResponse.Actions.FirstOrDefault();
            webhookWorkflowActionResponse.Headers.ShouldNotBeNull();
            webhookWorkflowActionResponse.Signature.ShouldNotBeNull();
            webhookWorkflowActionResponse.Id.ShouldNotBeNullOrEmpty();
            webhookWorkflowActionResponse.Url.ShouldNotBeNullOrEmpty();

            getWorkflowResponse.Conditions.ShouldNotBeNull();
            getWorkflowResponse.Conditions.Count.ShouldBe(2);
            Assert.True(getWorkflowResponse.Conditions.FirstOrDefault() is EventWorkflowConditionResponse);
            Assert.True(getWorkflowResponse.Conditions[1] is EntityWorkflowConditionResponse);
            getWorkflowResponse.GetLink("self").ShouldNotBeNull();

            GetWorkflowsResponse getWorkflowsResponse = await FourApi.WorkflowsClient().GetWorkflows();

            getWorkflowsResponse.ShouldNotBeNull();
            getWorkflowsResponse.Workflows.ShouldNotBeNull();
            getWorkflowsResponse.Workflows.ShouldNotBeEmpty();

            foreach (var workFlow in getWorkflowsResponse.Workflows)
            {
                Assert.Equal(nameWorkFlow, workFlow.Name);
                workFlow.Id.ShouldNotBeNullOrEmpty();
                workFlow.GetLink("self").ShouldNotBeNull();
            }
        }

        [Fact(Timeout = 180000, Skip = " ")]
        public async Task ShouldCreateAndUpdateWorkflow()
        {
            var createdWorkFlow = await CreateWorkflow();

            createdWorkFlow.ShouldNotBeNull();
            createdWorkFlow.Id.ShouldNotBeNull();

            UpdateWorkflowRequest request = new UpdateWorkflowRequest()
            {
                Name = "testing_2"
            };

            UpdateWorkflowResponse updateWorkflowResponse = await FourApi.WorkflowsClient().UpdateWorkflow(createdWorkFlow.Id, request);

            updateWorkflowResponse.ShouldNotBeNull();
            Assert.Equal("testing_2", updateWorkflowResponse.Name);
        }

        [Fact(Timeout = 180000, Skip = " ")]
        public async Task ShouldUpdateWorkflowAction()
        {
            var createdWorkFlow = await CreateWorkflow();

            createdWorkFlow.ShouldNotBeNull();
            createdWorkFlow.Id.ShouldNotBeNull();

            GetWorkflowResponse getWorkflowResponse = await FourApi.WorkflowsClient().GetWorkflow(createdWorkFlow.Id);

            getWorkflowResponse.ShouldNotBeNull();
            getWorkflowResponse.Id.ShouldNotBeNullOrEmpty();
            Assert.Equal(nameWorkFlow, getWorkflowResponse.Name);
            getWorkflowResponse.Actions.ShouldNotBeNull();
            getWorkflowResponse.Actions.Count.ShouldBe(1);
            string actionId = getWorkflowResponse.Actions.FirstOrDefault().Id;

            WebhookWorkflowActionRequest updateAction =
                 new WebhookWorkflowActionRequest("https://google.com/fail/fake",
                 new Dictionary<string, string>(),
                 new Actions.WebhookSignature() { Key = "8V8x0dLK%AyD*DNS8JJr", Method = "HMACSHA256" });

            await FourApi.WorkflowsClient().UpdateWorkflowAction(getWorkflowResponse.Id, actionId, updateAction);

            GetWorkflowResponse getWorkflowResponse2 = await FourApi.WorkflowsClient().GetWorkflow(createdWorkFlow.Id);

            getWorkflowResponse2.Actions.ShouldNotBeNull();
            getWorkflowResponse2.Actions.Count.ShouldBe(1);
            Assert.True(getWorkflowResponse2.Actions.FirstOrDefault() is WebhookWorkflowActionResponse);

            WebhookWorkflowActionResponse action = (WebhookWorkflowActionResponse)getWorkflowResponse2.Actions.FirstOrDefault();

            action.Headers.ShouldNotBeNull();
            action.Signature.ShouldNotBeNull();
            action.Id.ShouldNotBeNullOrEmpty();
            action.Url.ShouldNotBeNullOrEmpty();
        }

        [Fact(Timeout = 180000, Skip = " ")]
        public async Task ShouldUpdateWorkflowCondition()
        {
            var createdWorkFlow = await CreateWorkflow();

            createdWorkFlow.ShouldNotBeNull();
            createdWorkFlow.Id.ShouldNotBeNull();

            GetWorkflowResponse getWorkflowResponse = await FourApi.WorkflowsClient().GetWorkflow(createdWorkFlow.Id);

            getWorkflowResponse.ShouldNotBeNull();
            getWorkflowResponse.Id.ShouldNotBeNullOrEmpty();
            Assert.Equal(nameWorkFlow, getWorkflowResponse.Name);
            getWorkflowResponse.Conditions.ShouldNotBeNull();
            getWorkflowResponse.Conditions.Count.ShouldBe(2);

            EventWorkflowConditionResponse eventWorkflowConditionResponse = (EventWorkflowConditionResponse)getWorkflowResponse.Conditions.
                FirstOrDefault(x => x.Type.Equals(WorkflowConditionType.Event));

            eventWorkflowConditionResponse.ShouldNotBeNull();

            EventWorkflowConditionRequest updateEventCondition = new EventWorkflowConditionRequest(new Dictionary<string, ISet<string>>() {
                        {"gateway", new HashSet<string>{conditionsGateway} },
                        {"dispute", new HashSet<string>{conditionsDispute} }
                    });

            await FourApi.WorkflowsClient().UpdateWorkflowCondition(getWorkflowResponse.Id, eventWorkflowConditionResponse.Id, updateEventCondition);

            GetWorkflowResponse getWorkflowResponse2 = await FourApi.WorkflowsClient().GetWorkflow(createdWorkFlow.Id);

            getWorkflowResponse2.ShouldNotBeNull();
            getWorkflowResponse2.Conditions.ShouldNotBeNull();
            getWorkflowResponse2.Conditions.Count.ShouldBe(2);

            EventWorkflowConditionResponse updatedEventConditionResponse = (EventWorkflowConditionResponse)getWorkflowResponse2.Conditions.
              FirstOrDefault(x => x.Type.Equals(WorkflowConditionType.Event));

            updatedEventConditionResponse.ShouldNotBeNull();
            updatedEventConditionResponse.Id.ShouldNotBeNull();
            updatedEventConditionResponse.Events.ShouldNotBeNull();
        }
    }
}