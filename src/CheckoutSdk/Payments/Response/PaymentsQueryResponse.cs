using System.Collections.Generic;

namespace Checkout.Payments.Response
{
    public class PaymentsQueryResponse : HttpMetadata
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public int? TotalCount { get; set; }
    
        public IList<GetPaymentResponse> Data { get; set; }
    }
}
