using System.Collections.Generic;

namespace Checkout.Workflows.Four.Actions.Response
{
    public class WebhookWorkflowActionResponse : WorkflowActionResponse
    {
        public string Url { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public WebhookSignature Signature { get; set; }
    }
}