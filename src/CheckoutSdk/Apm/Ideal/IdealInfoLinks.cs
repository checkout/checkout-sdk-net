using System;
using System.Collections.Generic;
using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Apm.Ideal
{
    public sealed class IdealInfoLinks : IEquatable<IdealInfoLinks>
    {
        public Link Self { get; set; }

        public IList<CuriesLink> Curies { get; set; }

        [JsonProperty(PropertyName = "ideal:issuers")]
        public Link Issuers { get; set; }

        public bool Equals(IdealInfoLinks other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Self, other.Self) && Equals(Curies, other.Curies) && Equals(Issuers, other.Issuers);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is IdealInfoLinks other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Self, Curies, Issuers);
        }
    }
}