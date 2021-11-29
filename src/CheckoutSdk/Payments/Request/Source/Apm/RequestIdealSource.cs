using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestIdealSource : AbstractRequestSource, IEquatable<RequestIdealSource>
    {
        public RequestIdealSource() : base(PaymentSourceType.Ideal)
        {
        }

        public string Bic { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public bool Equals(RequestIdealSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Bic == other.Bic && Description == other.Description && Language == other.Language;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestIdealSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Bic, Description, Language);
        }
    }
}