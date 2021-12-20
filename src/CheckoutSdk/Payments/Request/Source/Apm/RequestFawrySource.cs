using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestFawrySource : AbstractRequestSource
    {
        public RequestFawrySource() : base(PaymentSourceType.Fawry)
        {
        }

        public string Description { get; set; }

        public string CustomerMobile { get; set; }

        public string CustomerEmail { get; set; }

        public IList<FawryProduct> Products { get; set; }
    }
}