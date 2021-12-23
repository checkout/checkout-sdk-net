using System.Collections.Generic;

namespace Checkout.Workflows.Four.Reflows
{
    public abstract class ReflowRequest
    {
        protected internal IList<string> Workflows { get; }

        protected internal ReflowRequest(IList<string> workflows)
        {
            this.Workflows = workflows;
        }
    }
}