using System;
using Newtonsoft.Json;

namespace Checkout.Apm.Ideal
{
    public sealed class IdealInfo : IEquatable<IdealInfo>
    {
        [JsonProperty(PropertyName = "_links")]
        public IdealInfoLinks Links { get; set; }

        public bool Equals(IdealInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Links, other.Links);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is IdealInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Links != null ? Links.GetHashCode() : 0);
        }
    }
}