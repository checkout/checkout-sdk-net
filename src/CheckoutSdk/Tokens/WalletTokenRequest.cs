namespace Checkout.Tokens
{
    public abstract class WalletTokenRequest
    {
        public TokenType? Type { get; }

        protected WalletTokenRequest(TokenType type)
        {
            Type = type;
        }
    }
}