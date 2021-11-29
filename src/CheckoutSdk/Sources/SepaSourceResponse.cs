using System;

namespace Checkout.Sources
{
    public sealed class SepaSourceResponse : SourceResponse, IEquatable<SepaSourceResponse>
    {
        public ResponseData ResponseData { get; set; }

        public bool Equals(SepaSourceResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(ResponseData, other.ResponseData);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SepaSourceResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (ResponseData != null ? ResponseData.GetHashCode() : 0);
        }
    }
}