using System;
using System.Collections.Generic;

namespace Checkout.Payments.Four
{
    public sealed class PaymentAction : IEquatable<PaymentAction>
    {
        public string Id { get; set; }

        public ActionType? Type { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public long? Amount { get; set; }

        public bool? Approved { get; set; }

        public string AuthCode { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public AuthorizationType? AuthorizationType { get; set; }

        public string Reference { get; set; }

        public Processing Processing { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public bool Equals(PaymentAction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Type == other.Type && ProcessedOn.Equals(other.ProcessedOn) &&
                   Amount == other.Amount && Approved == other.Approved && AuthCode == other.AuthCode &&
                   ResponseCode == other.ResponseCode && ResponseSummary == other.ResponseSummary &&
                   AuthorizationType == other.AuthorizationType && Reference == other.Reference &&
                   Equals(Processing, other.Processing) && Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentAction other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add((int) Type);
            hashCode.Add(ProcessedOn);
            hashCode.Add(Amount);
            hashCode.Add(Approved);
            hashCode.Add(AuthCode);
            hashCode.Add(ResponseCode);
            hashCode.Add(ResponseSummary);
            hashCode.Add((int) AuthorizationType);
            hashCode.Add(Reference);
            hashCode.Add(Processing);
            hashCode.Add(Metadata);
            return hashCode.ToHashCode();
        }
    }
}