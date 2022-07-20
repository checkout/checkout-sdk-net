using System.Collections.Generic;

namespace Checkout.Workflows.Conditions.Response
{
    public class ProcessingChannelWorkflowConditionResponse : WorkflowConditionResponse
    {
        public IList<string> ProcessingChannels { get; set; }
    }
}