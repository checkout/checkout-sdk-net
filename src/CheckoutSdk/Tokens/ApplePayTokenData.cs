using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public sealed class ApplePayTokenData : IEquatable<ApplePayTokenData>
    {
        public string Version { get; set; }

        public string Data { get; set; }

        public string Signature { get; set; }

        [JsonProperty("header")] private IDictionary<string, string> TokenHeader { get; set; }

        public bool Equals(ApplePayTokenData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Version == other.Version && Data == other.Data && Signature == other.Signature &&
                   Equals(TokenHeader, other.TokenHeader);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ApplePayTokenData) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Version, Data, Signature, TokenHeader);
        }
    }
}