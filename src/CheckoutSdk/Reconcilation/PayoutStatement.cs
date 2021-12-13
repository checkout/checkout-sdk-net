using System;

namespace Checkout.Reconciliation
{
    public sealed class PayoutStatement
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public double Currency { get; set; }

        public string PayoutFee { get; set; }

        public string NetAmount { get; set; }

        public string CarriedForwardAmount { get; set; }

        public string CurrentPeriodAmount { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }
    }
}