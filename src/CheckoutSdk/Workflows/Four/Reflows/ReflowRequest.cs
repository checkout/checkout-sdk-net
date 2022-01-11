using System.Collections.Generic;

namespace Checkout.Workflows.Four.Reflows
{
    public abstract class ReflowRequest
    {
        public IList<string> Workflows { get; set; }

        protected ReflowRequest(IList<string> workflows)
        {
            Workflows = workflows;
        }
    }
}