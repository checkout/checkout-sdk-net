namespace Checkout.Authentication.Standalone.Common.Responses.Acs
{
    /// <summary>
    /// acs
    /// The access control server (ACS) information. Can be empty if the session is still pending or if communication
    /// with the ACS failed. This will be available when the channel data and issuer fingerprint result have been
    /// provided.
    /// </summary>
    public class Acs
    {
        /// <summary>
        /// EMVCo-assigned unique identifier to track approved ACS
        /// [Required]
        /// <= 32
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Universally unique transaction identifier assigned by the ACS
        /// [Required]
        /// 36 characters
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// DS assigned ACS identifier
        /// [Required]
        /// <= 32
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// Indicates whether a challenge is required for the transaction to be authorized
        /// [Required]
        /// </summary>
        public bool ChallengeMandated { get; set; }

        /// <summary>
        /// Fully qualified URL of the ACS to be used for the challenge
        /// [Optional]
        /// <= 2048
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Contains the JSON web signature (JWS) compact serialization created by the ACS for a challenged app
        /// authentication. (Example has been truncated for readability.)
        /// [Optional]
        /// <= 512
        /// </summary>
        public string SignedContent { get; set; }

        /// <summary>
        /// The type of authentication as returned from the ACS provider.  • 01 = Static  • 02 = Dynamic  • 03 = OOB  •
        /// 04-79 = Reserved for EMVCo future use  • 80-99 = Reserved for DS use
        /// [Optional]
        /// ^\d{2}$
        /// </summary>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Indicator informing the ACS and the DS that the authentication has been cancelled
        /// [Optional]
        /// </summary>
        public ChallengeCancelReasonType? ChallengeCancelReason { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public InterfaceType? Interface { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public UiTemplateType? UiTemplate { get; set; }

        /// <summary>
        /// Indicator in the 3D Secure 2 RReq when the challenge is cancelled
        /// [Optional]
        /// [ 0 .. 99 ]
        /// </summary>
        public string ChallengeCancelReasonCode { get; set; }
    }
}