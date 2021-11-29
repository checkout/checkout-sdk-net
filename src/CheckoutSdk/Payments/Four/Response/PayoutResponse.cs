using System;
using Checkout.Common;

namespace Checkout.Payments.Four.Response
{
    public sealed class PayoutResponse : Resource, IEquatable<PayoutResponse>
    {
        public string Id { get; set; }

        public PaymentStatus? Status { get; set; }

        public string Reference { get; set; }

        public PaymentInstructionResponse Instruction { get; set; }

        public bool Equals(PayoutResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Status == other.Status && Reference == other.Reference &&
                   Equals(Instruction, other.Instruction);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PayoutResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, (int) Status, Reference, Instruction);
        }
    }
}