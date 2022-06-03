namespace Checkout.Sessions.Source
{
    public class SessionNetworkTokenSource : SessionSource
    {
        public string Token { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }
        
        public bool? Stored { get; set; }

        public SessionNetworkTokenSource() : base(SessionSourceType.NetworkToken)
        {
        }
    }
}