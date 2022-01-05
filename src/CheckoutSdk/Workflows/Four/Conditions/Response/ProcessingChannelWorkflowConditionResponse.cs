using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Response
{
    public class ProcessingChannelWorkflowConditionResponse : WorkflowConditionResponse
    {
        public IList<string> ProcessingChannels { get; set; }

        public ProcessingChannelWorkflowConditionResponse() : base(WorkflowConditionType.ProcessingChannel)
        {
        }
    }
}