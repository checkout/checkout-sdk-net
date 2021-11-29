using System;

namespace Checkout.Risk
{
    public sealed class Location : IEquatable<Location>
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Latitude == other.Latitude && Longitude == other.Longitude;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Location other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Latitude, Longitude);
        }
    }
}