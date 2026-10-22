namespace Checkout.Issuing.Cards.Requests.Suspend
{
    /// <summary>
    /// The optional request body; the whole body may be omitted.
    /// </summary>
    public class SuspendCardRequest
    {
        /// <summary>
        /// The reason for suspending the card.
        /// [Optional]
        /// </summary>
        public SuspendReason? Reason { get; set; }
    }
}