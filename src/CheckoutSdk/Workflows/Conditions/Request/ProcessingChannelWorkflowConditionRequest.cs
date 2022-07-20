using System.Collections.Generic;

namespace Checkout.Workflows.Conditions.Request
{
    public class ProcessingChannelWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IList<string> ProcessingChannels { get; set; }

        public ProcessingChannelWorkflowConditionRequest() : base(WorkflowConditionType.ProcessingChannel)
        {
        }
    }
}