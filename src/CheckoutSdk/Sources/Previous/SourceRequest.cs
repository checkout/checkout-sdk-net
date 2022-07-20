using Checkout.Common;

namespace Checkout.Sources.Previous
{
    public abstract class SourceRequest
    {
        public SourceType? Type { get; }

        protected SourceRequest(SourceType type)
        {
            Type = type;
        }

        public string Reference { get; set; }

        public Phone Phone { get; set; }

        public CustomerRequest Customer { get; set; }
    }
}