namespace Checkout.Issuing.Cards.Requests.Suspend
{
    /// <summary>
    /// The optional request body of POST /issuing/cards/{cardId}/suspend. The whole body may be
    /// omitted.
    /// </summary>
    public class SuspendCardRequest
    {
        /// <summary>
        /// The reason why the card is being suspended.
        /// [Optional]
        /// Enum: "suspected_lost" "suspected_stolen"
        /// </summary>
        public SuspendReason? Reason { get; set; }
    }
}
