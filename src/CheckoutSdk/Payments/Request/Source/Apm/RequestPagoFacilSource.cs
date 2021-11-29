using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestPagoFacilSource : AbstractRequestSource, IEquatable<RequestPagoFacilSource>
    {
        public RequestPagoFacilSource() : base(PaymentSourceType.PagoFacil)
        {
        }

        public IntegrationType IntegrationType { get; set; } = IntegrationType.Redirect;

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }

        public bool Equals(RequestPagoFacilSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IntegrationType == other.IntegrationType && Country == other.Country && Equals(Payer, other.Payer) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestPagoFacilSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) IntegrationType, Country, Payer, Description);
        }
    }
}