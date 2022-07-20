using Checkout.Common;

namespace Checkout.Workflows.Events
{
    public class SubjectEvent : Resource
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Timestamp { get; set; }
    }
}