using System;

namespace Checkout
{
    public sealed class OAuthAccessToken
    {
        public string Token { get; }
        private readonly DateTime? _expirationDate;

        public static OAuthAccessToken FromOAuthServiceResponse(OAuthServiceResponse response)
        {
            return new OAuthAccessToken(response.AccessToken,
                DateTime.Now.Add(TimeSpan.FromSeconds(response.ExpiresIn)));
        }

        private OAuthAccessToken(string token, DateTime expirationDate)
        {
            Token = token;
            _expirationDate = expirationDate;
        }

        public bool IsValid()
        {
            if (_expirationDate == null)
            {
                return false;
            }

            return _expirationDate > DateTime.Now;
        }
    }
}