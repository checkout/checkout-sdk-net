using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestRapiPagoSource : AbstractRequestSource, IEquatable<RequestRapiPagoSource>
    {
        public RequestRapiPagoSource() : base(PaymentSourceType.RapiPago)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }

        public bool Equals(RequestRapiPagoSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IntegrationType == other.IntegrationType && Country == other.Country && Equals(Payer, other.Payer) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestRapiPagoSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) IntegrationType, Country, Payer, Description);
        }
    }
}