using Checkout.Common;
using Checkout.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public sealed class ReconciliationPaymentReportResponse : EquatableResource<ReconciliationPaymentReportResponse>
    {
        public int Count { get; set; }

        public IList<PaymentReportData> Data { get; set; }

        public override bool EqualExp(ReconciliationPaymentReportResponse other)
            => Count.Equals(other.Count)
                && Data.EqualsNull(other.Data);

        public override int GetHashCode()
            => HashCode.Combine(Count, Data);
    }
}