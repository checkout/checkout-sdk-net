using Checkout.Common;
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
            => Type == other.Type && Date == other.Date;

        public override int GetHashCode()
            => HashCode.Combine(Type, Date);
    }
}