using System;
using Checkout.Common;

namespace Checkout.Risk.PreAuthentication
{
    public sealed class PreAuthenticationAssessmentResponse : Resource, IEquatable<PreAuthenticationAssessmentResponse>
    {
        public string AssessmentId { get; set; }

        public long? Score { get; set; }

        public PreAuthenticationResult Result { get; set; }

        public PreAuthenticationWarning Warning { get; set; }

        public bool Equals(PreAuthenticationAssessmentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AssessmentId == other.AssessmentId && Score == other.Score && Equals(Result, other.Result) &&
                   Equals(Warning, other.Warning);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PreAuthenticationAssessmentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AssessmentId, Score, Result, Warning);
        }
    }
}