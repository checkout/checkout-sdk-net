using Checkout.Workflows.Four.Actions.Request;
using Checkout.Workflows.Four.Conditions.Request;
using Checkout.Workflows.Four.Events;
using Checkout.Workflows.Four.Reflows;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Workflows.Four
{
    public class WorkflowsClientTest : UnitTestFixture
    {
        private const string WORKFLOWS_PATH = "workflows";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.FourOAuth, ValidFourSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.FourOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public WorkflowsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task ShouldCreateWorkflow()
        {
            CreateWorkflowRequest createWorkflowRequest = new CreateWorkflowRequest();
            CreateWorkflowResponse createWorkflowResponse = new CreateWorkflowResponse();

            _apiClient.Setup(apiClient =>
                apiClient.Post<CreateWorkflowResponse>(WORKFLOWS_PATH, _authorization,
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
                apiClient.Post<CreateWorkflowResponse>(WORKFLOWS_PATH, _authorization,
                createWorkflowRequest, CancellationToken.None, null))
               .ReturnsAsync(() => createWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.CreateWorkflow(null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("createWorkflowRequest cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldGetWorkflows()
        {
            GetWorkflowsResponse getWorkflowResponse = new GetWorkflowsResponse();

            _apiClient.Setup(apiClient =>
            apiClient.Get<GetWorkflowsResponse>(WORKFLOWS_PATH, _authorization, CancellationToken.None))
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
            apiClient.Get<GetWorkflowResponse>(WORKFLOWS_PATH + "/workflow_id", _authorization, CancellationToken.None))
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
            apiClient.Get<GetWorkflowResponse>(WORKFLOWS_PATH + "/workflow_id", _authorization, CancellationToken.None))
            .ReturnsAsync(() => getWorkflowResponse);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.GetWorkflow(null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldUpdateWorkflow()
        {
            UpdateWorkflowRequest updateWorkflowRequest = new UpdateWorkflowRequest();
            UpdateWorkflowResponse updateWorkflowResponse = new UpdateWorkflowResponse();

            _apiClient.Setup(apiClient =>
            apiClient.Patch<UpdateWorkflowResponse>(WORKFLOWS_PATH + "/workflow_id", _authorization, updateWorkflowRequest, CancellationToken.None, null))
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
                var response = await workflowsClient.UpdateWorkflow("", null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.UpdateWorkflow("123", null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("updateWorkflowRequest cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRemoveWorkflow()
        {
            var response = new Object();

            _apiClient.Setup(apiClient =>
            apiClient.Delete<Object>(WORKFLOWS_PATH + "/workflow_id", _authorization, CancellationToken.None))
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
                var response = await workflowsClient.RemoveWorkflow("");
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be blank", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldUpdateWorkflowAction()
        {
            var response = new Object();
            WebhookWorkflowActionRequest workflowActionRequest = new WebhookWorkflowActionRequest();

            _apiClient.Setup(apiClient =>
            apiClient.Put<Object>(WORKFLOWS_PATH + "/workflow_id/actions/action_id", _authorization, workflowActionRequest, CancellationToken.None, null))
            .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.UpdateWorkflowAction("workflow_id", "action_id", workflowActionRequest);

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailUpdateWorkflowAction_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.UpdateWorkflowAction("", "action_id", new WebhookWorkflowActionRequest());
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.UpdateWorkflowAction("12345", "", new WebhookWorkflowActionRequest());
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("actionId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.UpdateWorkflowAction("12345", "46562", null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowActionRequest cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldUpdateWorkflowCondition()
        {
            var response = new Object();
            EventWorkflowConditionRequest eventWorkflowConditionRequest = new EventWorkflowConditionRequest();

            _apiClient.Setup(apiClient =>
            apiClient.Put<Object>(WORKFLOWS_PATH + "/workflow_id/conditions/condition_id", _authorization, eventWorkflowConditionRequest, CancellationToken.None, null))
            .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.UpdateWorkflowCondition("workflow_id", "condition_id", eventWorkflowConditionRequest);

            getResponse.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldFailUpdateWorkflowCondition_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.UpdateWorkflowCondition("", "condition_id", new EventWorkflowConditionRequest());
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.UpdateWorkflowCondition("12345", "", new EventWorkflowConditionRequest());
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("conditionId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.UpdateWorkflowCondition("12345", "46562", null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowConditionRequest cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldGetEventTypes()
        {
            List<EventTypesResponse> response = new List<EventTypesResponse>();

            _apiClient.Setup(apiClient =>
            apiClient.Get<IList<EventTypesResponse>>(WORKFLOWS_PATH + "/event-types", _authorization, CancellationToken.None))
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
            apiClient.Get<SubjectEventsResponse>(WORKFLOWS_PATH + "/events/subject/subject_id", _authorization, CancellationToken.None))
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
                var response = await workflowsClient.GetSubjectEvents("  ");
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("subjectId cannot be blank", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldGetEvent()
        {
            GetEventResponse getEventResponse = new GetEventResponse();

            _apiClient.Setup(apiClient =>
            apiClient.Get<GetEventResponse>(WORKFLOWS_PATH + "/events/event_id", _authorization, CancellationToken.None))
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
                var response = await workflowsClient.GetEvent(null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("eventId cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldReflowByEvent()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                apiClient.Post<ReflowResponse>(WORKFLOWS_PATH + "/events/event_id", _authorization, null, CancellationToken.None, null))
               .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowByEvent("event_id");

            getResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowByEvent_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.ReflowByEvent("");
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("eventId cannot be blank", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldReflowBySubject()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                apiClient.Post<ReflowResponse>(WORKFLOWS_PATH + "/events/subject/subject_id/", _authorization, null, CancellationToken.None, null))
               .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowBySubject("subject_id");

            getResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowBySubject_invalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.ReflowBySubject(null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("subjectId cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldReflowByEventAndWorkflow()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                apiClient.Post<ReflowResponse>(WORKFLOWS_PATH + "/events/event_id/workflow/workflow_id/reflow/", _authorization, null, CancellationToken.None, null))
               .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowByEventAndWorkflow("event_id", "workflow_id");

            getResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowByEventAndWorkflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.ReflowByEventAndWorkflow(" ", "123");
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("eventId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.ReflowByEventAndWorkflow("123", null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldReflowBySubjectAndWorkflow()
        {
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                apiClient.Post<ReflowResponse>(WORKFLOWS_PATH + "/events/event_id/subject/subject_i/workflow/workflow_id/reflow", _authorization, null, CancellationToken.None, null))
               .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.ReflowBySubjectAndWorkflow("subject_id", "workflow_id");

            getResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldFailReflowBySubjectAndWorkflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.ReflowBySubjectAndWorkflow(" ", "123");
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("subjectId cannot be blank", ex.Message);
            }

            try
            {
                var response = await workflowsClient.ReflowBySubjectAndWorkflow("123", null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("workflowId cannot be null", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldReflow()
        {
            ReflowBySubjectsRequest reflowBySubjectsRequest = new ReflowBySubjectsRequest(new List<string>(), new List<string>());
            ReflowResponse response = new ReflowResponse();

            _apiClient.Setup(apiClient =>
                apiClient.Post<ReflowResponse>(WORKFLOWS_PATH + "/events/reflow", _authorization, null, CancellationToken.None, null))
               .ReturnsAsync(() => response);

            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            var getResponse = await workflowsClient.Reflow(reflowBySubjectsRequest);

            getResponse.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldFailReflow_InvalidParams()
        {
            IWorkflowsClient workflowsClient = new WorkflowsClient(_apiClient.Object, _configuration.Object);

            try
            {
                var response = await workflowsClient.Reflow(null);
            }
            catch (Exception ex)
            {
                Assert.True(ex is CheckoutArgumentException);
                Assert.Equal("reflowRequest cannot be null", ex.Message);
            }
        }
    }
}