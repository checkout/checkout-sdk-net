namespace Checkout.Sessions.Channel
{
    public class BrowserSession : ChannelData
    {
        public ThreeDsMethodCompletion? ThreeDsMethodCompletion { get; set; }

        public string AcceptHeader { get; set; }

        public bool? JavaEnabled { get; set; }
        
        public bool? JavascriptEnabled { get; set; }

        public string Language { get; set; }

        public string ColorDepth { get; set; }

        public string ScreenHeight { get; set; }

        public string ScreenWidth { get; set; }

        public string Timezone { get; set; }

        public string UserAgent { get; set; }

        public string IpAddress { get; set; }

        public BrowserSession() : base(ChannelType.Browser)
        {
        }
    }
}