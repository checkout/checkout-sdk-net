using System.Collections.Generic;

namespace Checkout.Workflows.Actions.Request
{
    public class WebhookWorkflowActionRequest : WorkflowActionRequest
    {
        public string Url { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public WebhookSignature Signature { get; set; }

        public WebhookWorkflowActionRequest() : base(WorkflowActionType.Webhook)
        {
        }
    }
}