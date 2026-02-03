using System.Collections.Generic;

namespace Checkout.Payments.Response
{
    public class PaymentsQueryResponse : HttpMetadata
    {
        /// <summary>
        /// The number of results to return per page.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The number of results to skip.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// The total number of payments matching the query.
        /// </summary>
        public int? TotalCount { get; set; }
    
        /// <summary>
        /// The list of payments matching the query.
        /// [Required]
        /// </summary>
        public IList<GetPaymentResponse> Data { get; set; }
    }
}
