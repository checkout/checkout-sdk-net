using Checkout.Common;
using Checkout.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public sealed class StatementReportResponse : EquatableResource<StatementReportResponse>
    {
        public int Count { get; set; }
        public IList<StatementData> Data { get; set; }

        public override bool EqualExp(StatementReportResponse other)
            => Count.Equals(other.Count)
                && Data.EqualsNull(other.Data);

        public override int GetHashCode()
            => HashCode.Combine(Count, Data);
    }
}