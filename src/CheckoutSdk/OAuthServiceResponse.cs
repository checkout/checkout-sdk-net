namespace Checkout
{
    public sealed class OAuthServiceResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public long ExpiresIn { get; set; }

        public string Error { get; set; }

        public bool IsValid() =>
            !string.IsNullOrWhiteSpace(AccessToken) &&
            !string.IsNullOrWhiteSpace(TokenType) &&
            ExpiresIn > 0 &&
            string.IsNullOrWhiteSpace(Error);
    }
}