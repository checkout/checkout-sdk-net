namespace Checkout.Sessions.Source
{
    public class RequestTokenSource : SessionSource
    {
        public string Token { get; set; }

        public RequestTokenSource() : base(SessionSourceType.Token)
        {
        }
    }
}