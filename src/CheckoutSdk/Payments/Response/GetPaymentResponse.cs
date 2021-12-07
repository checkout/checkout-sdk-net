using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Response.Destination;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Util;
using Newtonsoft.Json;

namespace Checkout.Payments.Response
{
    public sealed class GetPaymentResponse : Resource, IEquatable<GetPaymentResponse>
    {
        public string Id { get; set; }

        public DateTime? RequestedOn { get; set; }

        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]
        public IResponseSource Source { get; set; }

        [JsonConverter(typeof(PaymentResponseDestinationTypeConverter))]
        public IPaymentResponseDestination Destination { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public bool? Approved { get; set; }

        public PaymentStatus? Status { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsData ThreeDs { get; set; }

        public RiskAssessment Risk { get; set; }

        public CustomerResponse Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public string PaymentIp { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }

        public IList<PaymentActionSummary> Actions { get; set; }

        public bool Equals(GetPaymentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && RequestedOn.Equals(other.RequestedOn) && Equals(Source, other.Source) &&
                   Equals(Destination, other.Destination) && Amount == other.Amount && Currency == other.Currency &&
                   PaymentType == other.PaymentType && Reference == other.Reference &&
                   Description == other.Description && Approved == other.Approved && Status == other.Status &&
                   Equals(ThreeDs, other.ThreeDs) && Equals(Risk, other.Risk) && Equals(Customer, other.Customer) &&
                   Equals(BillingDescriptor, other.BillingDescriptor) && Equals(Shipping, other.Shipping) &&
                   PaymentIp == other.PaymentIp && Equals(Recipient, other.Recipient) &&
                   Equals(Metadata, other.Metadata) && Eci == other.Eci && SchemeId == other.SchemeId &&
                   Equals(Actions, other.Actions);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GetPaymentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(RequestedOn);
            hashCode.Add(Source);
            hashCode.Add(Destination);
            hashCode.Add(Amount);
            hashCode.Add(Currency);
            hashCode.Add((int) PaymentType);
            hashCode.Add(Reference);
            hashCode.Add(Description);
            hashCode.Add(Approved);
            hashCode.Add((int) Status);
            hashCode.Add(ThreeDs);
            hashCode.Add(Risk);
            hashCode.Add(Customer);
            hashCode.Add(BillingDescriptor);
            hashCode.Add(Shipping);
            hashCode.Add(PaymentIp);
            hashCode.Add(Recipient);
            hashCode.Add(Metadata);
            hashCode.Add(Eci);
            hashCode.Add(SchemeId);
            hashCode.Add(Actions);
            return hashCode.ToHashCode();
        }
    }
}