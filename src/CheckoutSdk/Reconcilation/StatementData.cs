using Checkout.Common;
using Checkout.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public sealed class StatementData : EquatableResource<StatementData>
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public IList<PayoutStatement> Payouts { get; set; }
        public DateTime PeriodEnd { get; set; }
        public DateTime PeriodStart { get; set; }

        public override bool EqualExp(StatementData other)
            => Id.EqualsNull(other.Id);

        public override int GetHashCode()
            => HashCode.Combine(Id);
    }
}