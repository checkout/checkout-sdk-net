using Checkout.Common;
using System;

namespace Checkout.Reconciliation.Previous
{
    public class PayoutStatement : Resource
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public Currency? Currency { get; set; }

        public string PayoutFee { get; set; }

        public string NetAmount { get; set; }

        public string CarriedForwardAmount { get; set; }

        public string CurrentPeriodAmount { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }
    }
}