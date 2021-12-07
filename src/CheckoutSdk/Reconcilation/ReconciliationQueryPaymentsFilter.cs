using Checkout.Common;
using Checkout.Common.Extensions;
using System;

namespace Checkout.Reconciliation
{
    public sealed class ReconciliationQueryPaymentsFilter : BaseEquatable<ReconciliationQueryPaymentsFilter>
    {
        public int? Limit { get; set; }
        public string Reference { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public override bool EqualExp(ReconciliationQueryPaymentsFilter other)
            => Limit.EqualsNull(other.Limit)
                && Reference.EqualsNull(other.Reference);

        public override int GetHashCode()
            => HashCode.Combine(
                    Limit,
                    Reference);
    }
}