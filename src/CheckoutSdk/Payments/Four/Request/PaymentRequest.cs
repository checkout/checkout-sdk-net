using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Sender;
using Newtonsoft.Json;

namespace Checkout.Payments.Four.Request
{
    public sealed class PaymentRequest : IEquatable<PaymentRequest>
    {
        public AbstractRequestSource Source { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public bool? MerchantInitiated { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public AuthorizationType? AuthorizationType { get; set; }

        public bool? Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public CustomerRequest Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsRequest ThreeDs { get; set; }

        public string ProcessingChannelId { get; set; }

        public string PreviousPaymentId { get; set; }

        public RiskRequest Risk { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string PaymentIp { get; set; }

        public PaymentSender Sender { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public MarketplaceData Marketplace { get; set; }

        public ProcessingSettings Processing { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public bool Equals(PaymentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Source, other.Source) && Amount == other.Amount && Currency == other.Currency &&
                   PaymentType == other.PaymentType && MerchantInitiated == other.MerchantInitiated &&
                   Reference == other.Reference && Description == other.Description &&
                   AuthorizationType == other.AuthorizationType && Capture == other.Capture &&
                   Nullable.Equals(CaptureOn, other.CaptureOn) && Equals(Customer, other.Customer) &&
                   Equals(BillingDescriptor, other.BillingDescriptor) && Equals(Shipping, other.Shipping) &&
                   Equals(ThreeDs, other.ThreeDs) && ProcessingChannelId == other.ProcessingChannelId &&
                   PreviousPaymentId == other.PreviousPaymentId && Equals(Risk, other.Risk) &&
                   SuccessUrl == other.SuccessUrl && FailureUrl == other.FailureUrl && PaymentIp == other.PaymentIp &&
                   Equals(Sender, other.Sender) && Equals(Recipient, other.Recipient) &&
                   Equals(Marketplace, other.Marketplace) && Equals(Processing, other.Processing) &&
                   Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Source);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add((int) PaymentType);
            hashCode.Add(MerchantInitiated);
            hashCode.Add(Reference);
            hashCode.Add(Description);
            hashCode.Add((int) AuthorizationType);
            hashCode.Add(Capture);
            hashCode.Add(CaptureOn);
            hashCode.Add(Customer);
            hashCode.Add(BillingDescriptor);
            hashCode.Add(Shipping);
            hashCode.Add(ThreeDs);
            hashCode.Add(ProcessingChannelId);
            hashCode.Add(PreviousPaymentId);
            hashCode.Add(Risk);
            hashCode.Add(SuccessUrl);
            hashCode.Add(FailureUrl);
            hashCode.Add(PaymentIp);
            hashCode.Add(Sender);
            hashCode.Add(Recipient);
            hashCode.Add(Marketplace);
            hashCode.Add(Processing);
            hashCode.Add(Metadata);
            return hashCode.ToHashCode();
        }
    }
}