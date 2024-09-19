using System;
using System.Collections.Generic;

namespace Checkout.Workflows.Actions.Response
{
    public class WorkflowActionInvocation
    {
        public string InvocationId { get; set; }

        public DateTime? Timestamp { get; set; }

        public bool? Retry { get; set; }

        public bool? Succeeded { get; set; }

        public bool? Final { get; set; }
        
        [Obsolete("This property will be removed in the future, and should not be used. Use ResultDetails instead.", false)]
        public IDictionary<string, object> Result { get; set; }

        public IDictionary<string, object> ResultDetails { get; set; }
    }
}