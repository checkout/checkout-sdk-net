using System;

namespace Checkout
{
    public sealed class OAuthAccessToken
    {
        public string Token { get; }
        public string TokenType { get; }
        private readonly DateTime? _expirationDate;

        public static OAuthAccessToken FromOAuthServiceResponse(OAuthServiceResponse response)
        {
            if (!response.IsValid())
            {
                throw new ArgumentException("Invalid OAuth response");
            }

            return new OAuthAccessToken(
                response.AccessToken,
                response.TokenType,
                DateTime.UtcNow.Add(TimeSpan.FromSeconds(response.ExpiresIn)));
        }

        private OAuthAccessToken(string token, string tokenType, DateTime expirationDate)
        {
            Token = !string.IsNullOrWhiteSpace(token) ? token : throw new ArgumentException("Token cannot be empty");
            TokenType = !string.IsNullOrWhiteSpace(tokenType)
                ? tokenType
                : throw new ArgumentException("TokenType cannot be empty");
            _expirationDate = expirationDate.ToUniversalTime();
        }

        public bool IsValid()
        {
            if (_expirationDate == null)
            {
                return false;
            }

            return _expirationDate > DateTime.UtcNow;
        }
    }
}