using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Response.Source
{
    public sealed class CurrencyAccountResponseSource : AbstractResponseSource, IResponseSource,
        IEquatable<CurrencyAccountResponseSource>
    {
        public long? Amount { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }

        public bool Equals(CurrencyAccountResponseSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CurrencyAccountResponseSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}