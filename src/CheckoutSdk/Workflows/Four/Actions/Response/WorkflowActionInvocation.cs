using System;
using System.Collections.Generic;

namespace Checkout.Workflows.Four.Actions.Response
{
    public class WorkflowActionInvocation
    {
        public string InvocationId { get; set; }

        public DateTime? Timestamp { get; set; }

        public bool? Retry { get; set; }

        public bool? Succeeded { get; set; }

        public bool? Final { get; set; }

        public IDictionary<string, object> Result { get; set; }
    }
}