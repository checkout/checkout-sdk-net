using System;
using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments.Links
{
    public sealed class PaymentLinkRequest : IEquatable<PaymentLinkRequest>
    {
        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public int? ExpiresIn { get; set; }

        public CustomerRequest Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public BillingInformation Billing { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public ProcessingSettings Processing { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsRequest ThreeDs { get; set; }

        public RiskRequest Risk { get; set; }

        public string ReturnUrl { get; set; }

        public string Locale { get; set; }

        public bool? Capture { get; set; }

        public DateTime? CaptureOn { get; set; }

        public bool Equals(PaymentLinkRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Currency == other.Currency && Reference == other.Reference &&
                   Description == other.Description && ExpiresIn == other.ExpiresIn &&
                   Equals(Customer, other.Customer) && Equals(Shipping, other.Shipping) &&
                   Equals(Billing, other.Billing) && Equals(Recipient, other.Recipient) &&
                   Equals(Processing, other.Processing) && Equals(Products, other.Products) &&
                   Equals(Metadata, other.Metadata) && Equals(ThreeDs, other.ThreeDs) && Equals(Risk, other.Risk) &&
                   ReturnUrl == other.ReturnUrl && Locale == other.Locale && Capture == other.Capture &&
                   Nullable.Equals(CaptureOn, other.CaptureOn);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentLinkRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(Reference);
            hashCode.Add(Description);
            hashCode.Add(ExpiresIn);
            hashCode.Add(Customer);
            hashCode.Add(Shipping);
            hashCode.Add(Billing);
            hashCode.Add(Recipient);
            hashCode.Add(Processing);
            hashCode.Add(Products);
            hashCode.Add(Metadata);
            hashCode.Add(ThreeDs);
            hashCode.Add(Risk);
            hashCode.Add(ReturnUrl);
            hashCode.Add(Locale);
            hashCode.Add(Capture);
            hashCode.Add(CaptureOn);
            return hashCode.ToHashCode();
        }
    }
}