using System;

namespace Checkout.Payments.Four.Request
{
    public sealed class PaymentInstruction : IEquatable<PaymentInstruction>
    {
        public string Purpose { get; set; }

        public string ChargeBearer { get; set; }

        public bool? Repair { get; set; }

        public InstructionScheme? Scheme { get; set; }

        public string QuoteId { get; set; }

        public bool Equals(PaymentInstruction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Purpose == other.Purpose && ChargeBearer == other.ChargeBearer && Repair == other.Repair &&
                   Scheme == other.Scheme && QuoteId == other.QuoteId;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentInstruction other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Purpose, ChargeBearer, Repair, (int) Scheme, QuoteId);
        }
    }
}