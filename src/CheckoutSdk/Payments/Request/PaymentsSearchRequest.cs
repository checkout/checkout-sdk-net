using Checkout.Common;

namespace Checkout.Payments.Request
{
    public class PaymentsSearchRequest : QueryFilterDateRange
    {
        /// <summary>
        /// The query string.
        /// For more information on how to build out your query, see the Search and filter payments documentation.
        /// [Optional]
        /// max 1024 characters
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The number of results to return per page.
        /// [Optional]
        /// Default: 10
        /// min 1
        /// max 1000
        /// </summary>
        public int? Limit { get; set; }
    }
}