using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Links
{
    public sealed class PaymentLinkDetailsResponse : Resource, IEquatable<PaymentLinkDetailsResponse>
    {
        public string Id { get; set; }

        public PaymentLinkStatus? Status { get; set; }

        public string ExpiresOn { get; set; }

        public string ReturnUrl { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public CustomerRequest Customer { get; set; }

        public BillingInformation Billing { get; set; }

        public IList<Product> Products { get; set; }

        public IDictionary<string, object> metadata { get; set; }

        public bool Equals(PaymentLinkDetailsResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && ExpiresOn.Equals(other.ExpiresOn) && ReturnUrl.Equals(other.ReturnUrl)
                   && Reference.Equals(other.Reference) && Description.Equals(other.Description);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CaptureResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ExpiresOn, ReturnUrl, Reference, Description);
        }
    }

}