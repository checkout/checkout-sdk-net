namespace Checkout.Sdk.Payments
{
    public class TokenSource : IPaymentSource
    {
        public const string TypeName = "token";

        public TokenSource(string token)
        {
            Token = token;
        }

        public string Token { get; }

        public string Type => TypeName;
    }
}