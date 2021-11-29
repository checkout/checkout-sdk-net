using System;
using Checkout.Common;

namespace Checkout.Risk.PreCapture
{
    public sealed class PreCaptureAssessmentResponse : Resource, IEquatable<PreCaptureAssessmentResponse>
    {
        public string AssessmentId { get; set; }

        public PreCaptureResult Result { get; set; }

        public PreCaptureWarning Warning { get; set; }

        public bool Equals(PreCaptureAssessmentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AssessmentId == other.AssessmentId && Equals(Result, other.Result) && Equals(Warning, other.Warning);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreCaptureAssessmentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AssessmentId, Result, Warning);
        }
    }
}