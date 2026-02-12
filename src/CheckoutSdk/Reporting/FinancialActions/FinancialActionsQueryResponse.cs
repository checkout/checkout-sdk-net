using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Financial
{
    public class FinancialActionsQueryResponse : Resource
    {
        public int? Count { get; set; }

        public int? Limit { get; set; }

        public IList<FinancialAction> Data { get; set; }
    }
}
