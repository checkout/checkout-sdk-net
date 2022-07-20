using Checkout.Common;

namespace Checkout.Workflows
{
    public class Workflow : Resource
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public bool? Active { get; set; }
    }
}