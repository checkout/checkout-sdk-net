using System;

namespace Checkout.Risk.PreCapture
{
    public sealed class AuthorizationResult : IEquatable<AuthorizationResult>
    {
        public string AvsCode { get; set; }

        public string CvvResult { get; set; }

        public bool Equals(AuthorizationResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AvsCode == other.AvsCode && CvvResult == other.CvvResult;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is AuthorizationResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AvsCode, CvvResult);
        }
    }
}