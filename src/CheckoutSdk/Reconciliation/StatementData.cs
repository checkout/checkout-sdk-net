using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public class StatementData
    {
        public string Id { get; set; }

        public DateTime? Date { get; set; }

        public IList<PayoutStatement> Payouts { get; set; }

        public DateTime? PeriodEnd { get; set; }

        public DateTime? PeriodStart { get; set; }
    }
}