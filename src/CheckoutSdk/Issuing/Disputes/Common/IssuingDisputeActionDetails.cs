namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// Provides instructions on the amendments required before the dispute can proceed, if the dispute
    /// status is <c>action_required</c>. Use the Amend an Issuing dispute endpoint to submit your amendments.
    /// [Beta]
    /// </summary>
    public class IssuingDisputeActionDetails
    {
        /// <summary>
        /// The amendments required before the dispute can proceed. For example, if you need to provide a
        /// reason code, or update the submitted evidence.
        /// [Optional]
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// Specifies whether the dispute was previously amended and an action response was provided.
        /// [Optional]
        /// </summary>
        public string LastActionResponse { get; set; }
    }
}
