using System;
using Checkout.Common;
using Checkout.Payments.Four.Request.Destination;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Sender;

namespace Checkout.Payments.Four.Request
{
    public sealed class PayoutRequest : IEquatable<PayoutRequest>
    {
        public PayoutRequestSource Source { get; set; }

        public PaymentRequestDestination Destination { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public PayoutBillingDescriptor BillingDescriptor { get; set; }

        public PaymentSender Sender { get; set; }

        public PaymentInstruction Instruction { get; set; }

        public string ProcessingChannelId { get; set; }

        public bool Equals(PayoutRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Source, other.Source) && Equals(Destination, other.Destination) && Amount == other.Amount &&
                   Currency == other.Currency && Reference == other.Reference &&
                   Equals(BillingDescriptor, other.BillingDescriptor) && Equals(Sender, other.Sender) &&
                   Equals(Instruction, other.Instruction) && ProcessingChannelId == other.ProcessingChannelId;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PayoutRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Source);
            hashCode.Add(Destination);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(Reference);
            hashCode.Add(BillingDescriptor);
            hashCode.Add(Sender);
            hashCode.Add(Instruction);
            hashCode.Add(ProcessingChannelId);
            return hashCode.ToHashCode();
        }
    }
}