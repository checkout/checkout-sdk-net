namespace Checkout.Four
{
    public sealed class OAuthServiceResponse
    {
        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }

        public string Error { get; set; }

        public bool IsValid()
        {
            return AccessToken != null && ExpiresIn != 0 && Error == null;
        }
    }
}