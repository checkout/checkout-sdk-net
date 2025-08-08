using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.BillingAddress;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.HomePhone;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.MobilePhone;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.WorkPhone;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.TokenSource
{
    /// <summary>
    /// token source Class
    /// The source of the authentication.
    /// </summary>
    public class TokenSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the TokenSource class.
        /// </summary>
        public TokenSource() : base(SourceType.Token)
        {
        }

        /// <summary>
        /// The Checkout.com card token
        /// [Required]
        /// <= 100
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Indicates the cardholder scheme choice
        /// [Optional]
        /// </summary>
        public SchemeType? Scheme { get; set; }

        /// <summary>
        /// The card number.
        /// [Optional]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The expiry month of the card.
        /// [Optional]
        /// [ 1 .. 2 ] characters [ 1 .. 12 ]
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year of the card.
        /// [Optional]
        /// 4 characters
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The customer's billing address. Any special characters will be replaced.
        /// [Optional]
        /// </summary>
        public BillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The cardholder's home phone number
        /// [Optional]
        /// </summary>
        public HomePhone HomePhone { get; set; }

        /// <summary>
        /// The cardholder's mobile phone number
        /// [Optional]
        /// </summary>
        public MobilePhone MobilePhone { get; set; }

        /// <summary>
        /// The cardholder's work phone number
        /// [Optional]
        /// </summary>
        public WorkPhone WorkPhone { get; set; }

        /// <summary>
        /// The email of the cardholder
        /// [Optional]
        /// <= 254
        /// </summary>
        public string Email { get; set; }
    }
}