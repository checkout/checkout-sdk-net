using System;

namespace Checkout.Risk.PreAuthentication
{
    public sealed class PreAuthenticationResult : IEquatable<PreAuthenticationResult>
    {
        public PreAuthenticationDecision? Decision { get; set; }

        public bool Equals(PreAuthenticationResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Decision == other.Decision;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreAuthenticationResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Decision.GetHashCode();
        }
    }
}