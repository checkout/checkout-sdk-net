

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class ThreeDSRequest
    {
        /// <summary>
        /// Whether to process the payment as a 3D Secure payment. Default: false
        /// </summary>
        public bool? Enabled { get; set; } = false;

        /// <summary>
        /// Applies only when 3ds.enabled is set to true. 
        /// Set to true to attempt the payment without 3DS when the issuer, card, or network doesn't support 3DS. Default: false
        /// </summary>
        public bool? AttemptN3d { get; set; } = false;

        /// <summary>
        /// Specifies the preference for whether a 3DS challenge should be performed. Default: "no_preference"
        /// </summary>
        public ChallengeIndicator? ChallengeIndicator { get; set; } = Entities.ChallengeIndicator.NoPreference;

        /// <summary>
        /// Specifies an exemption reason for the payment to not be processed using 3D Secure authentication.
        /// </summary>
        public ThreeDSExemption? Exemption { get; set; }

        /// <summary>
        /// Specifies whether to process the payment as 3D Secure, if authorization was soft declined 
        /// due to 3DS authentication being required. Default: true
        /// </summary>
        public bool? AllowUpgrade { get; set; } = true;
    }

    public enum ChallengeIndicator
    {
        NoPreference,
        
        NoChallengeRequested,
        
        ChallengeRequested,
        
        ChallengeRequestedMandate
    }

    public enum ThreeDSExemption
    {
        LowValue,
        
        TrustedListing,
        
        TrustedListingPrompt,
        
        TransactionRiskAssessment,
        
        ThreeDSOutage,
        
        ScaDelegation,
        
        OutOfScaScope,
        
        LowRiskProgram,
        
        RecurringOperation,
        
        DataShare,
        
        Other
    }
}