using Checkout.Common;
using Checkout.Workflows.Conditions;
using Checkout.Workflows.Conditions.Request;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows
{

    public class WorkflowConditionsIntegrationTest : AbstractWorkflowIntegrationTest
    {
        [Fact(Skip = "unstable")]
        public async Task ShouldAddAndRemoveWorkflowConditions()
        {
            CreateWorkflowResponse createWorkflowResponse = await CreateWorkflow();

            var workflowConditionRequest = new EntityWorkflowConditionRequest()
            {
                Type = WorkflowConditionType.Entity, 
                Entities = new List<string>()
                {
                    "ent_axclravnqf5u5ejkweijnp5zc4",
                    "ent_fidjosfjdisofdjsifdosfuzc4"
                }
            };

            IdResponse addWorkflowCondition =
                await DefaultApi.WorkflowsClient().AddWorkflowCondition(
                    createWorkflowResponse.Id,
                    workflowConditionRequest);
            addWorkflowCondition.ShouldNotBeNull();

            Exception exception = await Record.ExceptionAsync(() =>
                DefaultApi.WorkflowsClient().RemoveWorkflowCondition(
                    createWorkflowResponse.Id,
                    addWorkflowCondition.Id));
            Assert.Null(exception);
        }
    }
}