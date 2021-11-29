using System;
using System.Collections.Generic;

namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureWarning : IEquatable<PreCaptureWarning>
    {
        public PreCaptureDecision? Decision { get; set; }

        public List<string> Reasons { get; set; }

        public bool Equals(PreCaptureWarning other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Decision == other.Decision && Equals(Reasons, other.Reasons);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreCaptureWarning other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Decision, Reasons);
        }
    }
}