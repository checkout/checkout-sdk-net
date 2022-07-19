namespace Checkout.Sessions.Source
{
    public class SessionCardSource : SessionSource
    {
        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public SessionScheme? Scheme { get; set; }

        public bool? Stored { get; set; }
        
        public bool? StoreForFutureUse { get; set; }

        public SessionCardSource() : base(SessionSourceType.Card)
        {
        }
    }
}