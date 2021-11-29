using System;
using System.Collections;
using Checkout.Common;
using Checkout.Risk.source;

namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureAssessmentRequest : IEquatable<PreCaptureAssessmentRequest>
    {
        public string AssessmentId { get; set; }

        public DateTime? Date { get; set; }

        public RiskPaymentRequestSource Source { get; set; }

        public CustomerRequest Customer { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public RiskPayment Payment { get; set; }

        public RiskShippingDetails Shipping { get; set; }

        public Device Device { get; set; }

        public IDictionary Metadata { get; set; }

        public AuthenticationResult AuthenticationResult { get; set; }

        public AuthorizationResult AuthorizationResult { get; set; }

        public bool Equals(PreCaptureAssessmentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AssessmentId == other.AssessmentId && Nullable.Equals(Date, other.Date) &&
                   Equals(Source, other.Source) && Equals(Customer, other.Customer) && Amount == other.Amount &&
                   Currency == other.Currency && Equals(Payment, other.Payment) && Equals(Shipping, other.Shipping) &&
                   Equals(Device, other.Device) && Equals(Metadata, other.Metadata) &&
                   Equals(AuthenticationResult, other.AuthenticationResult) &&
                   Equals(AuthorizationResult, other.AuthorizationResult);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreCaptureAssessmentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(AssessmentId);
            hashCode.Add(Date);
            hashCode.Add(Source);
            hashCode.Add(Customer);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(Payment);
            hashCode.Add(Shipping);
            hashCode.Add(Device);
            hashCode.Add(Metadata);
            hashCode.Add(AuthenticationResult);
            hashCode.Add(AuthorizationResult);
            return hashCode.ToHashCode();
        }
    }
}