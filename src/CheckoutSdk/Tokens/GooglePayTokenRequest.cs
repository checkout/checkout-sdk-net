using System;
using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public sealed class GooglePayTokenRequest : TokenRequest, IEquatable<GooglePayTokenRequest>
    {
        public GooglePayTokenRequest() : base(TokenType.GooglePay)
        {
        }

        [JsonProperty("token_data")] public ApplePayTokenData TokenData { get; set; }

        public bool Equals(GooglePayTokenRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TokenData, other.TokenData);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GooglePayTokenRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (TokenData != null ? TokenData.GetHashCode() : 0);
        }
    }
}