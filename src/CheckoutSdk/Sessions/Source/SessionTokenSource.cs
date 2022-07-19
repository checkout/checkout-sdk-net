namespace Checkout.Sessions.Source
{
    public class SessionTokenSource : SessionSource
    {
        public string Token { get; set; }
        
        public bool? StoreForFutureUse { get; set; }

        public SessionTokenSource() : base(SessionSourceType.Token)
        {
        }
    }
}