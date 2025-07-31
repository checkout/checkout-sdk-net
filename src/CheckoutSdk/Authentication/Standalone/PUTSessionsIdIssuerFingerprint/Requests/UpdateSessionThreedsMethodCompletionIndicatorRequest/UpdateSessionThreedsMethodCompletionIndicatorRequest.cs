namespace Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Requests.
    UpdateSessionThreeDSMethodCompletionIndicatorRequest
{
    /// <summary>
    /// Update session 3DS Method completion indicator
    /// Update the session's 3DS Method completion indicator based on the result of accessing the 3DS Method URL.
    /// </summary>
    public class UpdateSessionThreedsMethodCompletionIndicatorRequest
    {
        /// <summary>
        /// The result of the 3DS method URL. Default to U if a response is not received from the 3DS Method URL within
        /// 10 seconds.
        /// [Required]
        /// 1 characters
        /// </summary>
        public ThreeDsMethodCompletionType? ThreeDsMethodCompletion { get; set; }
    }
}