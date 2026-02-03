namespace Checkout.Authentication.Standalone.Common.Responses.Threeds
{
    /// <summary>
    /// 3ds
    /// This object provides more information about the 3DS experience
    /// </summary>
    public class Threeds
    {
        /// <summary>
        /// The CReq message, encoded in Base 64.
        /// [Optional]
        /// </summary>
        public string ChallengeRequest { get; set; }

        /// <summary>
        /// The number of authentication attempts performed by the cardholder.
        /// [Optional]
        /// &lt;= 2
        /// </summary>
        public string InteractionCounter { get; set; }

        /// <summary>
        /// Provides additional information about the error returned.
        /// [Optional]
        /// </summary>
        public ErrorDetails.ErrorDetails ErrorDetails { get; set; }
    }
}