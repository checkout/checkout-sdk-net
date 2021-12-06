using Checkout.Common;
using Checkout.Common.Extensions;
using System;

namespace Checkout.Reconciliation
{
    public sealed class PayoutStatement : EquatableResource<PayoutStatement>
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public double Currency { get; set; }

        public string PayoutFee { get; set; }

        public string NetAmount { get; set; }

        public string CarriedForwardAmount { get; set; }

        public string CurrentPeriodAmount { get; set; }

        public DateTime Date { get; set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public override bool EqualExp(PayoutStatement other)
            => Id.EqualsNull(other.Id);

        public override int GetHashCode()
            => HashCode.Combine(Id);
    }
}