using System;
using System.Collections;
using Checkout.Common;
using Checkout.Risk.source;

namespace Checkout.Risk.PreAuthentication
{
    public sealed class PreAuthenticationAssessmentRequest : IEquatable<PreAuthenticationAssessmentRequest>
    {
        public DateTime? Date { get; set; }

        public RiskPaymentRequestSource Source { get; set; }

        public CustomerRequest Customer { get; set; }

        public RiskPayment Payment { get; set; }

        public RiskShippingDetails Shipping { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public Device Device { get; set; }

        public IDictionary Metadata { get; set; }

        public bool Equals(PreAuthenticationAssessmentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Nullable.Equals(Date, other.Date) && Equals(Source, other.Source) &&
                   Equals(Customer, other.Customer) && Equals(Payment, other.Payment) &&
                   Equals(Shipping, other.Shipping) && Reference == other.Reference &&
                   Description == other.Description && Amount == other.Amount && Currency == other.Currency &&
                   Equals(Device, other.Device) && Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreAuthenticationAssessmentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Date);
            hashCode.Add(Source);
            hashCode.Add(Customer);
            hashCode.Add(Payment);
            hashCode.Add(Shipping);
            hashCode.Add(Reference);
            hashCode.Add(Description);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(Device);
            hashCode.Add(Metadata);
            return hashCode.ToHashCode();
        }
    }
}