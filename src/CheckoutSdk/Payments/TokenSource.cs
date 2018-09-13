namespace Checkout.Sdk.Payments
{
    public class TokenSource : IPaymentSource
    {
        public TokenSource(string token)
        {
            Token = token;
        }

        public string Token { get; }

        public string Type => Consts.Source.Type.Token;
    }
}