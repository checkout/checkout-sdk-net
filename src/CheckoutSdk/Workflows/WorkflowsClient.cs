using Checkout.Common;
using Checkout.Workflows.Actions.Request;
using Checkout.Workflows.Actions.Response;
using Checkout.Workflows.Conditions.Request;
using Checkout.Workflows.Events;
using Checkout.Workflows.Reflows;
using System.Threading.Tasks;

namespace Checkout.Workflows
{
    public class WorkflowsClient : AbstractClient, IWorkflowsClient
    {
        private const string WorkflowsPath = "workflows";
        private const string WorkflowPath = "workflow";
        private const string ActionsPath = "actions";
        private const string ConditionsPath = "conditions";
        private const string EventTypesPath = "event-types";
        private const string EventsPath = "events";
        private const string ReflowPath = "reflow";
        private const string SubjectPath = "subject";
        private const string WorkflowId = "workflowId";

        public WorkflowsClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<CreateWorkflowResponse> CreateWorkflow(CreateWorkflowRequest createWorkflowRequest)
        {
            CheckoutUtils.ValidateParams("createWorkflowRequest", createWorkflowRequest);
            return ApiClient.Post<CreateWorkflowResponse>(WorkflowsPath, SdkAuthorization(), createWorkflowRequest);
        }

        public Task<GetWorkflowsResponse> GetWorkflows()
        {
            return ApiClient.Get<GetWorkflowsResponse>(WorkflowsPath, SdkAuthorization());
        }

        public Task<GetWorkflowResponse> GetWorkflow(string workflowId)
        {
            CheckoutUtils.ValidateParams(WorkflowId, workflowId);
            return ApiClient.Get<GetWorkflowResponse>(BuildPath(WorkflowsPath, workflowId), SdkAuthorization());
        }

        public Task<UpdateWorkflowResponse> UpdateWorkflow(string workflowId,
            UpdateWorkflowRequest updateWorkflowRequest)
        {
            CheckoutUtils.ValidateParams(
                WorkflowId, 
                workflowId, 
                "updateWorkflowRequest", 
                updateWorkflowRequest);
            return ApiClient.Patch<UpdateWorkflowResponse>(BuildPath(WorkflowsPath, workflowId), SdkAuthorization(),
                updateWorkflowRequest);
        }

        public Task<EmptyResponse> RemoveWorkflow(string workflowId)
        {
            CheckoutUtils.ValidateParams(WorkflowId, workflowId);
            return ApiClient.Delete<EmptyResponse>(BuildPath(WorkflowsPath, workflowId), SdkAuthorization());
        }

        public Task<IdResponse> AddWorkflowAction(string workflowId, WorkflowActionRequest workflowActionRequest)
        {
            CheckoutUtils.ValidateParams(
                WorkflowId, 
                workflowId, 
                "workflowActionRequest", 
                workflowActionRequest);
            return ApiClient.Post<IdResponse>(BuildPath(WorkflowsPath, workflowId, ActionsPath), 
                SdkAuthorization(), workflowActionRequest);
        }

        public Task<EmptyResponse> UpdateWorkflowAction(string workflowId, string actionId,
            WorkflowActionRequest workflowActionRequest)
        {
            CheckoutUtils.ValidateParams(
                WorkflowId, 
                workflowId, 
                "actionId", 
                actionId, 
                "workflowActionRequest",
                workflowActionRequest);
            return ApiClient.Put<EmptyResponse>(BuildPath(WorkflowsPath, workflowId, ActionsPath, actionId),
                SdkAuthorization(),
                workflowActionRequest);
        }
        
        public Task<EmptyResponse> RemoveWorkflowAction(string workflowId, string actionId)
        {
            CheckoutUtils.ValidateParams(WorkflowId, workflowId, "actionId", actionId);
            return ApiClient.Delete<EmptyResponse>(BuildPath(WorkflowsPath, workflowId, ActionsPath, actionId),
                SdkAuthorization());
        }

        public Task<IdResponse> AddWorkflowCondition(string workflowId, WorkflowConditionRequest workflowConditionRequest)
        {
            CheckoutUtils.ValidateParams(
                WorkflowId, 
                workflowId, 
                "workflowActionRequest", 
                workflowConditionRequest);
            return ApiClient.Post<IdResponse>(BuildPath(WorkflowsPath, workflowId, ConditionsPath), 
                SdkAuthorization(), workflowConditionRequest);
        }

        public Task<EmptyResponse> UpdateWorkflowCondition(string workflowId, string conditionId,
            WorkflowConditionRequest workflowConditionRequest)
        {
            CheckoutUtils.ValidateParams(WorkflowId, workflowId, "conditionId", conditionId,
                "workflowConditionRequest", workflowConditionRequest);
            return ApiClient.Put<EmptyResponse>(BuildPath(WorkflowsPath, workflowId, ConditionsPath, conditionId),
                SdkAuthorization(), workflowConditionRequest);
        }

        public Task<EmptyResponse> RemoveWorkflowCondition(string workflowId, string conditionId)
        {
            CheckoutUtils.ValidateParams(WorkflowId, workflowId, "conditionId", conditionId);
            return ApiClient.Delete<EmptyResponse>(BuildPath(WorkflowsPath, workflowId, ConditionsPath, conditionId),
                SdkAuthorization());
        }

        public Task<ItemsResponse<EventTypesResponse>> GetEventTypes()
        {
            return ApiClient.Get<ItemsResponse<EventTypesResponse>>(BuildPath(WorkflowsPath, EventTypesPath),
                SdkAuthorization());
        }

        public Task<SubjectEventsResponse> GetSubjectEvents(string subjectId)
        {
            CheckoutUtils.ValidateParams("subjectId", subjectId);
            return ApiClient.Get<SubjectEventsResponse>(BuildPath(WorkflowsPath, EventsPath, SubjectPath, subjectId),
                SdkAuthorization());
        }

        public Task<GetEventResponse> GetEvent(string eventId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId);
            return ApiClient.Get<GetEventResponse>(BuildPath(WorkflowsPath, EventsPath, eventId), SdkAuthorization());
        }

        public Task<ReflowResponse> ReflowByEvent(string eventId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId);
            return ApiClient.Post<ReflowResponse>(BuildPath(WorkflowsPath, EventsPath, eventId, ReflowPath),
                SdkAuthorization());
        }

        public Task<ReflowResponse> ReflowBySubject(string subjectId)
        {
            CheckoutUtils.ValidateParams("subjectId", subjectId);
            return ApiClient.Post<ReflowResponse>(
                BuildPath(WorkflowsPath, EventsPath, SubjectPath, subjectId, ReflowPath),
                SdkAuthorization());
        }

        public Task<ReflowResponse> ReflowByEventAndWorkflow(string eventId, string workflowId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId, WorkflowId, workflowId);
            return ApiClient.Post<ReflowResponse>(
                BuildPath(WorkflowsPath, EventsPath, eventId, WorkflowPath, workflowId, ReflowPath),
                SdkAuthorization());
        }

        public Task<ReflowResponse> ReflowBySubjectAndWorkflow(string subjectId, string workflowId)
        {
            CheckoutUtils.ValidateParams("subjectId", subjectId, WorkflowId, workflowId);
            return ApiClient.Post<ReflowResponse>(
                BuildPath(WorkflowsPath, EventsPath, SubjectPath, subjectId, WorkflowPath, workflowId, ReflowPath),
                SdkAuthorization());
        }

        public Task<ReflowResponse> Reflow(ReflowRequest reflowRequest)
        {
            CheckoutUtils.ValidateParams("reflowRequest", reflowRequest);
            return ApiClient.Post<ReflowResponse>(BuildPath(WorkflowsPath, EventsPath, ReflowPath), SdkAuthorization(),
                reflowRequest);
        }

        public Task<WorkflowActionInvocationsResponse> GetActionInvocations(string eventId, string actionId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId, "actionId", actionId);
            return ApiClient.Get<WorkflowActionInvocationsResponse>(
                BuildPath(WorkflowsPath, EventsPath, eventId, ActionsPath, actionId), SdkAuthorization());
        }
    }
}