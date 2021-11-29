using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Request.Destination;

namespace Checkout.Payments.Request
{
    public sealed class PayoutRequest : IEquatable<PayoutRequest>
    {
        public PaymentRequestDestination Destination { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public bool? Capture { get; set; } = false;

        public DateTime? CaptureOn { get; set; }

        public CustomerRequest Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public string PreviousPaymentId { get; set; }

        public RiskRequest Risk { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string PaymentIp { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public Purpose? Purpose { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public IDictionary<string, object> Processing { get; set; } = new Dictionary<string, object>();

        public bool Equals(PayoutRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Destination, other.Destination) && Amount == other.Amount && Currency == other.Currency &&
                   PaymentType == other.PaymentType && Reference == other.Reference &&
                   Description == other.Description && Capture == other.Capture &&
                   Nullable.Equals(CaptureOn, other.CaptureOn) && Equals(Customer, other.Customer) &&
                   Equals(BillingDescriptor, other.BillingDescriptor) && Equals(Shipping, other.Shipping) &&
                   PreviousPaymentId == other.PreviousPaymentId && Equals(Risk, other.Risk) &&
                   SuccessUrl == other.SuccessUrl && FailureUrl == other.FailureUrl && PaymentIp == other.PaymentIp &&
                   Equals(Recipient, other.Recipient) && Purpose == other.Purpose && Equals(Metadata, other.Metadata) &&
                   Equals(Processing, other.Processing);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PayoutRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Destination);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(PaymentType);
            hashCode.Add(Reference);
            hashCode.Add(Description);
            hashCode.Add(Capture);
            hashCode.Add(CaptureOn);
            hashCode.Add(Customer);
            hashCode.Add(BillingDescriptor);
            hashCode.Add(Shipping);
            hashCode.Add(PreviousPaymentId);
            hashCode.Add(Risk);
            hashCode.Add(SuccessUrl);
            hashCode.Add(FailureUrl);
            hashCode.Add(PaymentIp);
            hashCode.Add(Recipient);
            hashCode.Add(Purpose);
            hashCode.Add(Metadata);
            hashCode.Add(Processing);
            return hashCode.ToHashCode();
        }
    }
}