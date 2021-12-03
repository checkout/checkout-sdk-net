using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Links
{
    public sealed class PaymentLinkDetailsResponse : Resource, IEquatable<PaymentLinkDetailsResponse>
    {
        public string Id { get; set; }

        public PaymentLinkStatus? Status { get; set; }

        public string PaymentId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public CustomerResponse Customer { get; set; }

        public ShippingDetails Shipping { get; set; }

        public BillingInformation Billing { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public string ReturnUrl { get; set; }

        public string Locale { get; set; }

        public bool Equals(PaymentLinkDetailsResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Status == other.Status && PaymentId == other.PaymentId && Amount == other.Amount &&
                   Currency == other.Currency && Reference == other.Reference && Description == other.Description &&
                   Nullable.Equals(CreatedOn, other.CreatedOn) && Nullable.Equals(ExpiresOn, other.ExpiresOn) &&
                   Equals(Customer, other.Customer) && Equals(Shipping, other.Shipping) &&
                   Equals(Billing, other.Billing) && Equals(Products, other.Products) &&
                   Equals(Metadata, other.Metadata) && ReturnUrl == other.ReturnUrl && Locale == other.Locale;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentLinkDetailsResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Status);
            hashCode.Add(PaymentId);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add(Reference);
            hashCode.Add(Description);
            hashCode.Add(CreatedOn);
            hashCode.Add(ExpiresOn);
            hashCode.Add(Customer);
            hashCode.Add(Shipping);
            hashCode.Add(Billing);
            hashCode.Add(Products);
            hashCode.Add(Metadata);
            hashCode.Add(ReturnUrl);
            hashCode.Add(Locale);
            return hashCode.ToHashCode();
        }
    }
}