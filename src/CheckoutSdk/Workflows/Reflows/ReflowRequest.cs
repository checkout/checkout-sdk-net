using System.Collections.Generic;

namespace Checkout.Workflows.Reflows
{
    public abstract class ReflowRequest
    {
        public IList<string> Workflows { get; set; }
    }
}