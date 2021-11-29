using System;

namespace Checkout.Risk.PreCapture
{
    public sealed class AuthenticationResult : IEquatable<AuthenticationResult>
    {
        public bool? Attempted { get; set; }

        public bool? Challenged { get; set; }

        public bool? Succeeded { get; set; }

        public bool? LiabilityShifted { get; set; }

        public string Method { get; set; }

        public string Version { get; set; }

        public bool Equals(AuthenticationResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Attempted == other.Attempted && Challenged == other.Challenged && Succeeded == other.Succeeded &&
                   LiabilityShifted == other.LiabilityShifted && Method == other.Method && Version == other.Version;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is AuthenticationResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Attempted, Challenged, Succeeded, LiabilityShifted, Method, Version);
        }
    }
}