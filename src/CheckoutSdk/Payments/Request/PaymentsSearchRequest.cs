using Checkout.Common;

namespace Checkout.Payments.Request
{
    public class PaymentsSearchRequest : QueryFilterDateRange
    {
        /// <summary>
        /// The query string.
        /// For more information on how to build out your query, see the Search and filter payments documentation.
        /// &lt;= 1024
        /// </summary>
        public string Query { get; set; }
        
        /// <summary>
        /// Default: 10
        /// The number of results to return per page.
        /// [ 1 .. 1000 ]
        /// </summary>
        public int? Limit { get; set; }
    }
}