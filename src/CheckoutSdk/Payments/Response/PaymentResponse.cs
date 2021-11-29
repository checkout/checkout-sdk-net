using System;
using Checkout.Common;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Util;
using Newtonsoft.Json;

namespace Checkout.Payments.Response
{
    public sealed class PaymentResponse : Resource, IEquatable<PaymentResponse>
    {
        public string ActionId { get; set; }

        public long? Amount { get; set; }

        public bool? Approved { get; set; }

        public string AuthCode { get; set; }

        public string Id { get; set; }

        public Currency? Currency { get; set; }

        public CustomerResponse Customer { get; set; }

        [JsonConverter(typeof(PaymentResponseSourceTypeConverter))]
        public IResponseSource Source { get; set; }

        public PaymentStatus? Status { get; set; }

        [JsonProperty(PropertyName = "3ds")] public ThreeDsEnrollment ThreeDs { get; set; }

        public string Reference { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }

        public RiskAssessment Risk { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public PaymentProcessing Processing { get; set; }

        public string Eci { get; set; }

        public string SchemeId { get; set; }

        public bool Equals(PaymentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ActionId == other.ActionId && Amount == other.Amount && Approved == other.Approved &&
                   AuthCode == other.AuthCode && Id == other.Id && Currency == other.Currency &&
                   Equals(Customer, other.Customer) && Equals(Source, other.Source) && Status == other.Status &&
                   Equals(ThreeDs, other.ThreeDs) && Reference == other.Reference &&
                   ResponseCode == other.ResponseCode && ResponseSummary == other.ResponseSummary &&
                   Equals(Risk, other.Risk) && Nullable.Equals(ProcessedOn, other.ProcessedOn) &&
                   Equals(Processing, other.Processing) && Eci == other.Eci && SchemeId == other.SchemeId;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ActionId);
            hashCode.Add(Amount);
            hashCode.Add(Approved);
            hashCode.Add(AuthCode);
            hashCode.Add(Id);
            hashCode.Add(Currency);
            hashCode.Add(Customer);
            hashCode.Add(Source);
            hashCode.Add((int) Status);
            hashCode.Add(ThreeDs);
            hashCode.Add(Reference);
            hashCode.Add(ResponseCode);
            hashCode.Add(ResponseSummary);
            hashCode.Add(Risk);
            hashCode.Add(ProcessedOn);
            hashCode.Add(Processing);
            hashCode.Add(Eci);
            hashCode.Add(SchemeId);
            return hashCode.ToHashCode();
        }
    }
}