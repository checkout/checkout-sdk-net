using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Request
{
    public class ProcessingChannelWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IList<string> ProcessingChannels { get; set; }

        public ProcessingChannelWorkflowConditionRequest() : base(WorkflowConditionType.ProcessingChannel)
        {
        }
    }
}