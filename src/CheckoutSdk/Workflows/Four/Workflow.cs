using Checkout.Common;

namespace Checkout.Workflows.Four
{
    public class Workflow : Resource
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public bool? Active { get; set; }
    }
}