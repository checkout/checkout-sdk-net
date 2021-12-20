using Checkout.Common;

namespace Checkout.Sources
{
    public class SepaSourceRequest : SourceRequest
    {
        public SepaSourceRequest() : base(SourceType.Sepa)
        {
        }

        public Address BillingAddress { get; set; }

        public SourceData SourceData { get; set; }
    }
}