using Checkout.Common;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.MobilePhone
{
    /// <summary>
    /// mobile_phone
    /// The cardholder's mobile phone number
    /// </summary>
    public class MobilePhone
    {
        /// <summary>
        /// Country code. According to ITU-E.164
        /// [Required]
        /// [ 1 .. 3 ] characters
        /// ^\d{1,3}$
        /// </summary>
        public CountryCode? CountryCode { get; set; }

        /// <summary>
        /// The rest of the number. According to ITU-E.164
        /// [Required]
        /// ^\d{1,15}$
        /// <= 15
        /// </summary>
        public string Number { get; set; }
    }
}