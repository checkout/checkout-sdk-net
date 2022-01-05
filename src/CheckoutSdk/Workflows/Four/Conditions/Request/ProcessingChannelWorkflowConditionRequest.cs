using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Request
{
    public class ProcessingChannelWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IList<string> ProcessingChannels { get; }

        public ProcessingChannelWorkflowConditionRequest() : base(WorkflowConditionType.ProcessingChannel)
        {
        }

        public ProcessingChannelWorkflowConditionRequest(IList<string> processingChannels) : base(WorkflowConditionType.ProcessingChannel)
        {
            this.ProcessingChannels = processingChannels;
        }
    }
}