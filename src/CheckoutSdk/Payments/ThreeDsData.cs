using System;

namespace Checkout.Payments
{
    public sealed class ThreeDsData : IEquatable<ThreeDsData>
    {
        public bool? Downgraded { get; set; }

        public string Enrolled { get; set; }

        public string SignatureValid { get; set; }

        public string AuthenticationResponse { get; set; }

        public string Cryptogram { get; set; }

        public string Xid { get; set; }

        public string Version { get; set; }

        public Exemption? Exemption { get; set; }

        public bool Equals(ThreeDsData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Downgraded == other.Downgraded && Enrolled == other.Enrolled &&
                   SignatureValid == other.SignatureValid && AuthenticationResponse == other.AuthenticationResponse &&
                   Cryptogram == other.Cryptogram && Xid == other.Xid && Version == other.Version &&
                   Exemption == other.Exemption;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ThreeDsData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Downgraded, Enrolled, SignatureValid, AuthenticationResponse, Cryptogram, Xid,
                Version, Exemption);
        }
    }
}