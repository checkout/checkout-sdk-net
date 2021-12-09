using System;
using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public sealed class ApplePayTokenRequest : WalletTokenRequest, IEquatable<ApplePayTokenRequest>
    {
        public ApplePayTokenRequest() : base(TokenType.ApplePay)
        {
        }

        [JsonProperty("token_data")] public ApplePayTokenData TokenData { get; set; }

        public bool Equals(ApplePayTokenRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TokenData, other.TokenData);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ApplePayTokenRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (TokenData != null ? TokenData.GetHashCode() : 0);
        }
    }
}