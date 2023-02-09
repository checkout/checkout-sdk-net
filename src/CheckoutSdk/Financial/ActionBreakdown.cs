using Checkout.Common;
using System;

namespace Checkout.Financial
{
    public class ActionBreakdown
    {
        public string BreakdownType { get; set; }

        public double? FxRateApplied { get; set; }

        public Currency? HoldingCurrency { get; set; }

        public double? HoldingCurrencyAmount { get; set; }

        public Currency? ProcessingCurrency { get; set; }

        public double? ProcessingCurrencyAmount { get; set; }

        public Currency? TransactionCurrency { get; set; }

        public double? TransactionCurrencyAccount { get; set; }

        public double? ProcessingToTransactionCurrencyFxRate { get; set; }

        public double? TransactionToHoldingCurrencyFxRate { get; set; }

        public string FeeDetail { get; set; }

        public string ReserveRate { get; set; }

        public DateTime? ReserveReleaseDate { get; set; }

        public DateTime? ReserveDeductedDate { get; set; }

        public double? TaxFxRate { get; set; }

        public Currency? EntityCountryTaxCurrency { get; set; }

        public double? TaxCurrencyAmount { get; set; }
    }
}
