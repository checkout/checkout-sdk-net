using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public class StatementReportResponse : Resource
    {
        public int? Count { get; set; }

        public IList<StatementData> Data { get; set; }
    }
}