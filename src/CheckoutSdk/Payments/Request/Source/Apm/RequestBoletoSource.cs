using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestBoletoSource : AbstractRequestSource, IEquatable<RequestBoletoSource>
    {
        public RequestBoletoSource() : base(PaymentSourceType.Boleto)
        {
        }

        public IntegrationType? IntegrationType { get; set; }

        public CountryCode? Country { get; set; }

        public Payer Payer { get; set; }

        public string Description { get; set; }

        public bool Equals(RequestBoletoSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IntegrationType == other.IntegrationType && Country == other.Country && Equals(Payer, other.Payer) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestBoletoSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IntegrationType, Country, Payer, Description);
        }
    }
}