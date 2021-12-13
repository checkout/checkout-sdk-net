using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public sealed class ReconciliationPaymentReportResponse
    {
        public int? Count { get; set; }

        public IList<PaymentReportData> Data { get; set; }
    }
}