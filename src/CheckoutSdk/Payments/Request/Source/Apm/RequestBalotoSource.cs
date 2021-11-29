using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestBalotoSource : AbstractRequestSource, IEquatable<RequestBalotoSource>
    {
        public RequestBalotoSource() : base(PaymentSourceType.Baloto)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public BalotoPayer Payer { get; set; }

        public string Description { get; set; }

        public bool Equals(RequestBalotoSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IntegrationType == other.IntegrationType && Country == other.Country && Equals(Payer, other.Payer) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestBalotoSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) IntegrationType, Country, Payer, Description);
        }
    }
}