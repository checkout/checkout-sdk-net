using Checkout.Common;

namespace Checkout.Sources
{
    public abstract class SourceResponse : Resource
    {
        public string Id { get; set; }

        public SourceType? Type { get; set; }

        public string ResponseCode { get; set; }

        public CustomerResponse Customer { get; set; }
    }
}