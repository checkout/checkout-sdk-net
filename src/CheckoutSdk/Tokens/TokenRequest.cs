namespace Checkout.Tokens
{
    public abstract class TokenRequest
    {
        public TokenType? Type { get; }

        protected TokenRequest(TokenType type)
        {
            Type = type;
        }
    }
}