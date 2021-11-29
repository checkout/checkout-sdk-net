using System;

namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureResult : IEquatable<PreCaptureResult>
    {
        public PreCaptureDecision? Decision { get; set; }

        public string Details { get; set; }

        public bool Equals(PreCaptureResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Decision == other.Decision && Details == other.Details;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreCaptureResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Decision, Details);
        }
    }
}