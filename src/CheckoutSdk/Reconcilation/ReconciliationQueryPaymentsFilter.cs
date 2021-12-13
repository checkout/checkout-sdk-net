using System;

namespace Checkout.Reconciliation
{
    public sealed class ReconciliationQueryPaymentsFilter
    {
        public int? Limit { get; set; }

        public string Reference { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}