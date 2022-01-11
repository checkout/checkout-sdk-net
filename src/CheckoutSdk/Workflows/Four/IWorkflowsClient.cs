using Checkout.Workflows.Four.Actions.Request;
using Checkout.Workflows.Four.Conditions.Request;
using Checkout.Workflows.Four.Events;
using Checkout.Workflows.Four.Reflows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Workflows.Four
{
    public interface IWorkflowsClient
    {
        Task<CreateWorkflowResponse> CreateWorkflow(CreateWorkflowRequest createWorkflowRequest);

        Task<GetWorkflowsResponse> GetWorkflows();

        Task<GetWorkflowResponse> GetWorkflow(string workflowId);

        Task<UpdateWorkflowResponse> UpdateWorkflow(string workflowId, UpdateWorkflowRequest updateWorkflowRequest);

        Task<object> RemoveWorkflow(string workflowId);

        Task<object> UpdateWorkflowAction(string workflowId, string actionId,
            WorkflowActionRequest workflowActionRequest);

        Task<object> UpdateWorkflowCondition(string workflowId, string conditionId,
            WorkflowConditionRequest workflowConditionRequest);

        Task<IList<EventTypesResponse>> GetEventTypes();

        Task<SubjectEventsResponse> GetSubjectEvents(string subjectId);

        Task<GetEventResponse> GetEvent(string eventId);

        Task<ReflowResponse> ReflowByEvent(string eventId);

        Task<ReflowResponse> ReflowBySubject(string subjectId);

        Task<ReflowResponse> ReflowByEventAndWorkflow(string eventId, string workflowId);

        Task<ReflowResponse> ReflowBySubjectAndWorkflow(string subjectId, string workflowId);

        Task<ReflowResponse> Reflow(ReflowRequest reflowRequest);
    }
}