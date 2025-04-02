using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Issuing.Transactions.Responses.Query
{
    public class TransactionsListResponse : HttpMetadata
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public int? TotalCount { get; set; }

        public IList<TransactionSingleResponse> Data { get; set; }
    }
}