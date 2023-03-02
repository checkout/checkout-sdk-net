using Checkout.Common;
using Checkout.Workflows.Actions.Request;
using Checkout.Workflows.Actions.Response;
using Checkout.Workflows.Conditions.Request;
using Checkout.Workflows.Events;
using Checkout.Workflows.Reflows;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Checkout.Workflows
{
    public class WorkflowsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public WorkflowsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        public async Task ShouldCreateWorkflow()
        {
            CreateWorkflowRequest createWorkflowRequest = new CreateWorkflowRequest();
            CreateWorkflowResponse createWorkflowResponse = new CreateWorkflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CreateWorkflowResponse>("workflows", _authorization,
                        createWorkflowRequest, CancellationToken.None, null))
                .ReturnsAsync(() => createWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var response = await workflowsClient.CreateWorkflow(createWorkflowRequest);

            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailCreateWorkflow_InvalidParams()
        {
            CreateWorkflowRequest createWorkflowRequest = new CreateWorkflowRequest();
            CreateWorkflowResponse createWorkflowResponse = new CreateWorkflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<CreateWorkflowResponse>("workflows", _authorization,
                        createWorkflowRequest, CancellationToken.None, null))
                .ReturnsAsync(() => createWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.CreateWorkflow(null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("createWorkflowRequest cannot be null");
            }
        }

        [Fact]
        public async Task ShouldGetWorkflows()
        {
            GetWorkflowsResponse getWorkflowResponse = new GetWorkflowsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetWorkflowsResponse>("workflows", _authorization, CancellationToken.None))
                .ReturnsAsync(() => getWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var response = await workflowsClient.GetWorkflows();

            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldGetWorkflowById()
        {
            GetWorkflowResponse getWorkflowResponse = new GetWorkflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetWorkflowResponse>("workflows" + "/workflow_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => getWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var response = await workflowsClient.GetWorkflow("workflow_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailGetWorkflowById_InvalidParams()
        {
            GetWorkflowResponse getWorkflowResponse = new GetWorkflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetWorkflowResponse>("workflows" + "/workflow_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => getWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.GetWorkflow(null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be null");
            }
        }

        [Fact]
        public async Task ShouldUpdateWorkflow()
        {
            UpdateWorkflowRequest updateWorkflowRequest = new UpdateWorkflowRequest();
            UpdateWorkflowResponse updateWorkflowResponse = new UpdateWorkflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<UpdateWorkflowResponse>("workflows" + "/workflow_id", _authorization,
                        updateWorkflowRequest, CancellationToken.None, null))
                .ReturnsAsync(() => updateWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var response = await workflowsClient.UpdateWorkflow("workflow_id", updateWorkflowRequest);

            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailUpdateWorkflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.UpdateWorkflow("", null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }

            try
            {
                await workflowsClient.UpdateWorkflow("123", null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("updateWorkflowRequest cannot be null");
            }
        }

        [Fact]
        public async Task ShouldRemoveWorkflow()
        {
            var response = new EmptyResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>("workflows" + "/workflow_id", _authorization, CancellationToken.None))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.RemoveWorkflow("workflow_id");

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailRemoveWorkflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.RemoveWorkflow("");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }
        }
        
        [Fact]
        public async Task ShouldAddWorkflowAction()
        {
            WebhookWorkflowActionRequest workflowActionRequest = new WebhookWorkflowActionRequest();
            IdResponse response = new IdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdResponse>("workflows" + "/workflow_id" + "/actions", _authorization,
                        workflowActionRequest, CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            IdResponse getResponse =
                await workflowsClient.AddWorkflowAction("workflow_id", workflowActionRequest);

            getResponse.ShouldNotBeNull();
        }
        
        [Fact]
        public async Task ShouldFailAddWorkflowAction_InvalidParams()
        {
            WebhookWorkflowActionRequest workflowActionRequest = new WebhookWorkflowActionRequest();
            IdResponse response = new IdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdResponse>("workflows" + "/workflow_id" + "/actions", _authorization,
                        workflowActionRequest, CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.AddWorkflowAction("",null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }
        }

        [Fact]
        public async Task ShouldUpdateWorkflowAction()
        {
            WebhookWorkflowActionRequest workflowActionRequest = new WebhookWorkflowActionRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<EmptyResponse>("workflows" + "/workflow_id/actions/action_id", _authorization,
                        workflowActionRequest, CancellationToken.None, null))
                .ReturnsAsync(() => new EmptyResponse());

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse =
                await workflowsClient.UpdateWorkflowAction("workflow_id", "action_id", workflowActionRequest);

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailUpdateWorkflowAction_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.UpdateWorkflowAction("", "action_id", new WebhookWorkflowActionRequest());
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }

            try
            {
                await workflowsClient.UpdateWorkflowAction("12345", "", new WebhookWorkflowActionRequest());
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("actionId cannot be blank");
            }

            try
            {
                await workflowsClient.UpdateWorkflowAction("12345", "46562", null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowActionRequest cannot be null");
            }
        }
        
        [Fact]
        public async Task ShouldRemoveWorkflowAction()
        {
            EmptyResponse response = new EmptyResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>(
                        "workflows" + "/workflow_id" + "/actions" + "/action_id", 
                        _authorization, 
                        CancellationToken.None))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            EmptyResponse getResponse = await workflowsClient.RemoveWorkflowAction("workflow_id", "action_id");

            getResponse.ShouldNotBeNull();
        }
        
        [Fact]
        public async Task ShouldFailRemoveWorkflowAction_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.RemoveWorkflowAction("", "");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }
        }
        
        [Fact]
        public async Task ShouldAddWorkflowCondition()
        {
            EntityWorkflowConditionRequest workflowConditionRequest = new EntityWorkflowConditionRequest();
            IdResponse response = new IdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdResponse>("workflows" + "/workflow_id" + "/conditions", _authorization,
                        workflowConditionRequest, CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            IdResponse getResponse =
                await workflowsClient.AddWorkflowCondition("workflow_id", workflowConditionRequest);

            getResponse.ShouldNotBeNull();
        }
        
        [Fact]
        public async Task ShouldFailAddWorkflowCondition_InvalidParams()
        {
            EntityWorkflowConditionRequest workflowConditionRequest = new EntityWorkflowConditionRequest();
            IdResponse response = new IdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdResponse>("workflows" + "/workflow_id" + "/conditions", _authorization,
                        workflowConditionRequest, CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.AddWorkflowCondition("",null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }
        }

        [Fact]
        public async Task ShouldUpdateWorkflowCondition()
        {
            EventWorkflowConditionRequest eventWorkflowConditionRequest = new EventWorkflowConditionRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<EmptyResponse>("workflows" + "/workflow_id/conditions/condition_id", _authorization,
                        eventWorkflowConditionRequest, CancellationToken.None, null))
                .ReturnsAsync(() => new EmptyResponse());

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var updateResponse =
                await workflowsClient.UpdateWorkflowCondition("workflow_id", "condition_id",
                    eventWorkflowConditionRequest);

            updateResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailUpdateWorkflowCondition_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.UpdateWorkflowCondition("", "condition_id",
                    new EventWorkflowConditionRequest());
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }

            try
            {
                await workflowsClient.UpdateWorkflowCondition("12345", "", new EventWorkflowConditionRequest());
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("conditionId cannot be blank");
            }

            try
            {
                await workflowsClient.UpdateWorkflowCondition("12345", "46562", null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowConditionRequest cannot be null");
            }
        }
        
        [Fact(Skip="Unestable")]
        public async Task ShouldRemoveWorkflowConditions()
        {
            EmptyResponse response = new EmptyResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>(
                        "workflows" + "/workflow_id" + "/condition" + "/condition_id", 
                        _authorization, 
                        CancellationToken.None))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            EmptyResponse getResponse = await workflowsClient.RemoveWorkflowCondition("workflow_id", "condition_id");

            getResponse.ShouldNotBeNull();
        }
        
        [Fact]
        public async Task ShouldFailRemoveWorkflowConditions_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.RemoveWorkflowCondition("", "");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be blank");
            }
        }

        [Fact]
        public async Task ShouldGetEventTypes()
        {
            ItemsResponse<EventTypesResponse> response = new ItemsResponse<EventTypesResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ItemsResponse<EventTypesResponse>>("workflows" + "/event-types", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.GetEventTypes();

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldGetSubjectEvents()
        {
            SubjectEventsResponse subjectEventsResponse = new SubjectEventsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<SubjectEventsResponse>("workflows" + "/events/subject/subject_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => subjectEventsResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var response = await workflowsClient.GetSubjectEvents("subject_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailGetSubjectEvents_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.GetSubjectEvents("  ");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("subjectId cannot be blank");
            }
        }

        [Fact]
        public async Task ShouldGetEvent()
        {
            GetEventResponse getEventResponse = new GetEventResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetEventResponse>("workflows/events/event_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => getEventResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var response = await workflowsClient.GetEvent("event_id");

            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailGetEvent_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.GetEvent(null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("eventId cannot be null");
            }
        }

        [Fact]
        public async Task ShouldReflowByEvent()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReflowResponse>("workflows/events/event_id/reflow", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowByEvent("event_id");

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowByEvent_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.ReflowByEvent("");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("eventId cannot be blank");
            }
        }

        [Fact]
        public async Task ShouldReflowBySubject()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReflowResponse>("workflows/events/subject/subject_id/reflow", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowBySubject("subject_id");

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowBySubject_invalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.ReflowBySubject(null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("subjectId cannot be null");
            }
        }

        [Fact]
        public async Task ShouldReflowByEventAndWorkflow()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReflowResponse>("workflows/events/event_id/workflow/workflow_id/reflow",
                        _authorization, null, CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowByEventAndWorkflow("event_id", "workflow_id");

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowByEventAndWorkflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.ReflowByEventAndWorkflow(" ", "123");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("eventId cannot be blank");
            }

            try
            {
                await workflowsClient.ReflowByEventAndWorkflow("123", null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be null");
            }
        }

        [Fact]
        public async Task ShouldReflowBySubjectAndWorkflow()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReflowResponse>(
                        "workflows/events/subject/subject_id/workflow/workflow_id/reflow",
                        _authorization, null, CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowBySubjectAndWorkflow("subject_id", "workflow_id");

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowBySubjectAndWorkflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.ReflowBySubjectAndWorkflow(" ", "123");
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("subjectId cannot be blank");
            }

            try
            {
                await workflowsClient.ReflowBySubjectAndWorkflow("123", null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("workflowId cannot be null");
            }
        }

        [Fact]
        public async Task ShouldReflow()
        {
            ReflowBySubjectsRequest reflowBySubjectsRequest =
                new ReflowBySubjectsRequest {Subjects = new List<string>(), Workflows = new List<string>()};
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReflowResponse>("workflows/events/reflow", _authorization, reflowBySubjectsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.Reflow(reflowBySubjectsRequest);

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailReflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                await workflowsClient.Reflow(null);
                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("reflowRequest cannot be null");
            }
        }

        [Fact]
        public async Task ShouldGetActionInvocations()
        {
            WorkflowActionInvocationsResponse response = new WorkflowActionInvocationsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<WorkflowActionInvocationsResponse>("workflows/events/eventId/actions/actionId",
                        _authorization, CancellationToken.None))
                .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.GetActionInvocations("eventId", "actionId");

            getResponse.ShouldNotBeNull();
        }
    }
}