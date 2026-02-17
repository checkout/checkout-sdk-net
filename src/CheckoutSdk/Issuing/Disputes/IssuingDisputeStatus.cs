using System.Runtime.Serialization;

namespace Checkout.Issuing.Disputes
{
    /// <summary>
    /// The status of the issuing dispute.
    /// [Beta]
    /// </summary>
    public enum IssuingDisputeStatus
    {
        /// <summary>
        /// The dispute has been created and is ready for processing.
        /// </summary>
        [EnumMember(Value = "created")] Created,
        
        /// <summary>
        /// The dispute has been canceled and will not proceed further.
        /// </summary>
        [EnumMember(Value = "canceled")] Canceled,
        
        /// <summary>
        /// The dispute is currently being processed by the card scheme.
        /// </summary>
        [EnumMember(Value = "processing")] Processing,
        
        /// <summary>
        /// Action is required from the issuer to proceed with the dispute.
        /// </summary>
        [EnumMember(Value = "action_required")] ActionRequired,
        
        /// <summary>
        /// The dispute has been resolved in favor of the issuer.
        /// </summary>
        [EnumMember(Value = "won")] Won,
        
        /// <summary>
        /// The dispute has been resolved in favor of the merchant.
        /// </summary>
        [EnumMember(Value = "lost")] Lost
    }
}