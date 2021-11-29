using System;

namespace Checkout.Payments
{
    public sealed class ProcessingSettings : IEquatable<ProcessingSettings>
    {
        public bool? Aft { get; set; }

        public bool Equals(ProcessingSettings other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Aft == other.Aft;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ProcessingSettings other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Aft.GetHashCode();
        }
    }
}