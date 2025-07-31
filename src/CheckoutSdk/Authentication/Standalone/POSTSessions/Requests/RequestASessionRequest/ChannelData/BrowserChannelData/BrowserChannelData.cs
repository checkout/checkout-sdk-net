using Checkout.Authentication.Standalone.Common.Requests;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData
{
    /// <summary>
    /// browser channel_data Class
    /// The information gathered from the environment used to initiate the session
    /// </summary>
    public class BrowserChannelData : AbstractChannelData
    {
        /// <summary>
        /// Initializes a new instance of the BrowserChannelData class.
        /// </summary>
        public BrowserChannelData() : base(ChannelDataType.Browser)
        {
        }

        /// <summary>
        /// Exact content of the HTTP accept headers as sent to the 3DS Requestor from the cardholder’s browser
        /// [Required]
        /// <= 2048
        /// </summary>
        public string AcceptHeader { get; set; }

        /// <summary>
        /// Boolean that represents the ability of the cardholder's browser to execute Java. Value is returned from the
        /// navigator.javaEnabled property.
        /// [Required]
        /// </summary>
        public bool JavaEnabled { get; set; }

        /// <summary>
        /// Default:  true Boolean that represents the ability of the cardholder's browser to execute Javascript. Value
        /// is returned from the navigator.javascriptEnabled property. *only applicable/required for authentication
        /// performed using 3DS 2.2. If authentications results in processing on 2.1 or lower, this field will be
        /// disregarded.
        /// [Required]
        /// </summary>
        public bool JavascriptEnabled { get; set; } = true;

        /// <summary>
        /// Value representing the browser language as defined in IETF BCP47. Returned from the navigator.language
        /// property.
        /// [Required]
        /// [ 1 .. 12 ] characters
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Value representing the bit depth of the color palette for displaying images, in bits per pixel. Obtained
        /// from the cardholder's browser from the screen.colorDepth property.
        /// [Required]
        /// [ 1 .. 2 ] characters
        /// </summary>
        public string ColorDepth { get; set; }

        /// <summary>
        /// Total height of the cardholder’s screen in pixels. Value is returned from the screen.height property.
        /// [Required]
        /// [ 1 .. 6 ] characters
        /// </summary>
        public string ScreenHeight { get; set; }

        /// <summary>
        /// Total width of the cardholder’s screen in pixels. Value is returned from the screen.width property.
        /// [Required]
        /// [ 1 .. 6 ] characters
        /// </summary>
        public string ScreenWidth { get; set; }

        /// <summary>
        /// Time difference between UTC time and the local time of the cardholder's browser, in minutes.
        /// [Required]
        /// [ 1 .. 5 ] characters
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// Exact content of the HTTP user-agent header
        /// [Required]
        /// <= 2048
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// IP address of the browser as returned by the HTTP headers to the 3DS Requestor
        /// [Required]
        /// <= 45
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Default: "U" Indicates whether the 3DS Method successfully completed • Y = Successfully completed • N =
        /// Did not successfully complete • U = Unavailable (3DS Method URL was not present in the preperation response
        /// (PRes) message data for the card range associated with the cardholder's account number)
        /// [Optional]
        /// 1 characters
        /// </summary>
        public ThreeDsMethodCompletionType ThreeDsMethodCompletion { get; set; } = ThreeDsMethodCompletionType.U;

        /// <summary>
        /// Whether the Payment API is enabled for all parent frames. This is required for Google SPA support in hosted
        /// sessions.
        /// [Optional]
        /// </summary>
        public bool IframePaymentAllowed { get; set; }

        /// <summary>
        /// The raw Sec-CH-UA header value. This can improve Google SPA support.
        /// [Optional]
        /// </summary>
        public string UserAgentClientHint { get; set; }
    }
}