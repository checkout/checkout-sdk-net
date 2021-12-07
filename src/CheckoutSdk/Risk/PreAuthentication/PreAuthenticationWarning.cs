using System;
using System.Collections.Generic;

namespace Checkout.Risk.PreAuthentication
{
    public sealed class PreAuthenticationWarning : IEquatable<PreAuthenticationWarning>
    {
        public PreAuthenticationDecision? Decision { get; set; }

        public IList<string> Reasons { get; set; }

        public bool Equals(PreAuthenticationWarning other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Decision == other.Decision && Equals(Reasons, other.Reasons);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreAuthenticationWarning other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Decision, Reasons);
        }
    }
}