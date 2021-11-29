using System;

namespace Checkout.Risk
{
    public sealed class Device : IEquatable<Device>
    {
        public string Ip { get; set; }

        public Location Location { get; set; }

        public string Os { get; set; }

        public string Type { get; set; }

        public string Model { get; set; }

        public DateTime? Date { get; set; }

        public string UserAgent { get; set; }

        public string Fingerprint { get; set; }

        public bool Equals(Device other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Ip == other.Ip && Equals(Location, other.Location) && Os == other.Os && Type == other.Type &&
                   Model == other.Model && Nullable.Equals(Date, other.Date) && UserAgent == other.UserAgent &&
                   Fingerprint == other.Fingerprint;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Device other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ip, Location, Os, Type, Model, Date, UserAgent, Fingerprint);
        }
    }
}