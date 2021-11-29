using System;
using Checkout.Common;

namespace Checkout.Disputes
{
    public sealed class PaymentDispute : IEquatable<PaymentDispute>
    {
        public string Id { get; set; }

        public string ActionId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Method { get; set; }

        public string Arn { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public bool Equals(PaymentDispute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && ActionId == other.ActionId && Amount == other.Amount &&
                   Currency == other.Currency && Method == other.Method && Arn == other.Arn &&
                   Nullable.Equals(ProcessedOn, other.ProcessedOn);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentDispute other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ActionId, Amount, Currency, Method, Arn, ProcessedOn);
        }
    }
}