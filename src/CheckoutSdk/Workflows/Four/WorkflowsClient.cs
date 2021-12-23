using Checkout.Workflows.Four.Actions.Request;
using Checkout.Workflows.Four.Conditions.Request;
using Checkout.Workflows.Four.Events;
using Checkout.Workflows.Four.Reflows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Workflows.Four
{
    public class WorkflowsClient : AbstractClient, IWorkflowsClient
    {
        private const string WORKFLOWS_PATH = "workflows";
        private const string WORKFLOW = "workflow";
        private const string ACTIONS = "actions";
        private const string CONDITIONS = "conditions";
        private const string EVENT_TYPES = "event-types";
        private const string EVENTS = "events";
        private const string REFLOW = "reflow";
        private const string SUBJECT = "subject";
        private const string WORKFLOW_ID = "workflowId";

        private IList<EventTypesResponse> EventTypesResponse = new List<EventTypesResponse>();

        public WorkflowsClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<CreateWorkflowResponse> CreateWorkflow(CreateWorkflowRequest createWorkflowRequest)
        {
            CheckoutUtils.ValidateParams("createWorkflowRequest", createWorkflowRequest);
            return ApiClient.Post<CreateWorkflowResponse>(WORKFLOWS_PATH, SdkAuthorization(), createWorkflowRequest);
        }

        public Task<GetWorkflowsResponse> GetWorkflows()
        {
            return ApiClient.Get<GetWorkflowsResponse>(WORKFLOWS_PATH, SdkAuthorization());
        }

        public Task<GetWorkflowResponse> GetWorkflow(string workflowId)
        {
            CheckoutUtils.ValidateParams(WORKFLOW_ID, workflowId);
            return ApiClient.Get<GetWorkflowResponse>(BuildPath(WORKFLOWS_PATH, workflowId), SdkAuthorization());
        }

        public Task<UpdateWorkflowResponse> UpdateWorkflow(string workflowId, UpdateWorkflowRequest updateWorkflowRequest)
        {
            CheckoutUtils.ValidateParams(WORKFLOW_ID, workflowId, "updateWorkflowRequest", updateWorkflowRequest);
            return ApiClient.Patch<UpdateWorkflowResponse>(BuildPath(WORKFLOWS_PATH, workflowId), SdkAuthorization(), updateWorkflowRequest);
        }

        public Task<object> RemoveWorkflow(string workflowId)
        {
            CheckoutUtils.ValidateParams(WORKFLOW_ID, workflowId);
            return ApiClient.Delete<object>(BuildPath(WORKFLOWS_PATH, workflowId), SdkAuthorization());
        }

        public Task<object> UpdateWorkflowAction(string workflowId, string actionId, WorkflowActionRequest workflowActionRequest)
        {
            CheckoutUtils.ValidateParams(WORKFLOW_ID, workflowId, "actionId", actionId, "workflowActionRequest", workflowActionRequest);
            return ApiClient.Put<object>(BuildPath(WORKFLOWS_PATH, workflowId, ACTIONS, actionId), SdkAuthorization(), workflowActionRequest);
        }

        public Task<Object> UpdateWorkflowCondition(string workflowId, string conditionId, WorkflowConditionRequest workflowConditionRequest)
        {
            CheckoutUtils.ValidateParams(WORKFLOW_ID, workflowId, "conditionId", conditionId, "workflowConditionRequest", workflowConditionRequest);
            return ApiClient.Put<object>(BuildPath(WORKFLOWS_PATH, workflowId, CONDITIONS, conditionId), SdkAuthorization(), workflowConditionRequest);
        }

        public Task<IList<EventTypesResponse>> GetEventTypes()
        {
            return ApiClient.Get<IList<EventTypesResponse>>(BuildPath(WORKFLOWS_PATH, EVENT_TYPES), SdkAuthorization());
        }

        public Task<SubjectEventsResponse> GetSubjectEvents(string subjectId)
        {
            CheckoutUtils.ValidateParams("subjectId", subjectId);
            return ApiClient.Get<SubjectEventsResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, SUBJECT, subjectId), SdkAuthorization());
        }

        public Task<GetEventResponse> GetEvent(string eventId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId);
            return ApiClient.Get<GetEventResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, eventId), SdkAuthorization());
        }

        public Task<ReflowResponse> ReflowByEvent(string eventId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId);
            return ApiClient.Post<ReflowResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, eventId, REFLOW), SdkAuthorization(), null);
        }

        public Task<ReflowResponse> ReflowBySubject(string subjectId)
        {
            CheckoutUtils.ValidateParams("subjectId", subjectId);
            return ApiClient.Post<ReflowResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, SUBJECT, subjectId, REFLOW), SdkAuthorization(), null);
        }

        public Task<ReflowResponse> ReflowByEventAndWorkflow(string eventId, string workflowId)
        {
            CheckoutUtils.ValidateParams("eventId", eventId, WORKFLOW_ID, workflowId);
            return ApiClient.Post<ReflowResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, eventId, WORKFLOW, workflowId, REFLOW), SdkAuthorization(), null);
        }

        public Task<ReflowResponse> ReflowBySubjectAndWorkflow(string subjectId, string workflowId)
        {
            CheckoutUtils.ValidateParams("subjectId", subjectId, WORKFLOW_ID, workflowId);
            return ApiClient.Post<ReflowResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, SUBJECT, subjectId, WORKFLOW, workflowId, REFLOW), SdkAuthorization(), null);
        }

        public Task<ReflowResponse> Reflow(ReflowRequest reflowRequest)
        {
            CheckoutUtils.ValidateParams("reflowRequest", reflowRequest);
            return ApiClient.Post<ReflowResponse>(BuildPath(WORKFLOWS_PATH, EVENTS, REFLOW), SdkAuthorization(), reflowRequest);
        }
    }
}