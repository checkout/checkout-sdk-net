using Checkout.Common;
using Checkout.Common.Extensions;
using System;

namespace Checkout.Reconciliation
{
    public sealed class Breakdown : BaseEquatable<Breakdown>
    {
        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string ProcessingCurrencyAmount { get; set; }

        public string PayoutCurrencyAmount { get; set; }

        public override bool EqualExp(Breakdown other) 
            => Type.EqualsNull(other.Type) 
                && Date.Equals(other.Date) 
                && ProcessingCurrencyAmount.EqualsNull(other.ProcessingCurrencyAmount)
                && PayoutCurrencyAmount.EqualsNull(other.PayoutCurrencyAmount);

        public override int GetHashCode()
            => HashCode.Combine(Type, Date, ProcessingCurrencyAmount, PayoutCurrencyAmount);
    }
}