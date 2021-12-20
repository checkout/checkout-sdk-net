using System;

namespace Checkout.Reconciliation
{
    public class Breakdown
    {
        public string Type { get; set; }

        public DateTime? Date { get; set; }

        public string ProcessingCurrencyAmount { get; set; }

        public string PayoutCurrencyAmount { get; set; }
    }
}