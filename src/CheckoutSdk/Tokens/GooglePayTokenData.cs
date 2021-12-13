using System;
using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public sealed class GooglePayTokenData : IEquatable<GooglePayTokenData>
    {
        public string Signature { get; set; }

        [JsonProperty(PropertyName = "protocolVersion")]
        public string ProtocolVersion { get; set; }

        [JsonProperty(PropertyName = "signedMessage")]
        public string SignedMessage { get; set; }

        public bool Equals(GooglePayTokenData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Signature == other.Signature && ProtocolVersion == other.ProtocolVersion &&
                   SignedMessage == other.SignedMessage;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GooglePayTokenData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Signature, ProtocolVersion, SignedMessage);
        }
    }
}