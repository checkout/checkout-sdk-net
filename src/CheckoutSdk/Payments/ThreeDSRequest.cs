using System;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public sealed class ThreeDsRequest : IEquatable<ThreeDsRequest>
    {
        public bool? Enabled { get; set; } = true;

        [JsonProperty(PropertyName = "attempt_n3d")]
        public bool? AttemptN3D { get; set; }

        public string Eci { get; set; }

        public string Cryptogram { get; set; }

        public string Xid { get; set; }

        public string Version { get; set; }

        public Exemption? Exemption { get; set; }

        public bool Equals(ThreeDsRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Enabled == other.Enabled && AttemptN3D == other.AttemptN3D && Eci == other.Eci &&
                   Cryptogram == other.Cryptogram && Xid == other.Xid && Version == other.Version &&
                   Exemption == other.Exemption;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ThreeDsRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Enabled, AttemptN3D, Eci, Cryptogram, Xid, Version, Exemption);
        }
    }
}