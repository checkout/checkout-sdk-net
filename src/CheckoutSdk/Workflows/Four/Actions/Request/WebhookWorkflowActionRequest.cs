using System.Collections.Generic;

namespace Checkout.Workflows.Four.Actions.Request
{
    public class WebhookWorkflowActionRequest : WorkflowActionRequest
    {
        public string Url { get; }

        public IDictionary<string, string> Headers { get; }

        public WebhookSignature Signature { get; }

        public WebhookWorkflowActionRequest() : base(WorkflowActionType.Webhook)
        {
        }

        public WebhookWorkflowActionRequest(string url, IDictionary<string, string> headers, WebhookSignature signature)
            : base(WorkflowActionType.Webhook)
        {
            this.Url = url;
            this.Headers = headers;
            this.Signature = signature;
        }
    }
}