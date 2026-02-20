namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for attempt responses
    /// </summary>
    public abstract class BaseAttemptResponse<TStatus> : BaseWithCodesResponse
    {
        /// <summary>
        /// The attempt status
        /// </summary>
        public TStatus Status { get; set; }
    }
}