using System;

namespace Checkout.Apm.Klarna
{
    public sealed class KlarnaShippingInfo : IEquatable<KlarnaShippingInfo>
    {
        public string ShippingCompany { get; set; }

        public string ShippingMethod { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingUri { get; set; }

        public string ReturnShippingCompany { get; set; }

        public string ReturnTrackingNumber { get; set; }

        public string ReturnTrackingUri { get; set; }

        public bool Equals(KlarnaShippingInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ShippingCompany == other.ShippingCompany && ShippingMethod == other.ShippingMethod &&
                   TrackingNumber == other.TrackingNumber && TrackingUri == other.TrackingUri &&
                   ReturnShippingCompany == other.ReturnShippingCompany &&
                   ReturnTrackingNumber == other.ReturnTrackingNumber && ReturnTrackingUri == other.ReturnTrackingUri;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is KlarnaShippingInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ShippingCompany, ShippingMethod, TrackingNumber, TrackingUri, ReturnShippingCompany,
                ReturnTrackingNumber, ReturnTrackingUri);
        }
    }
}