namespace Checkout.Issuing.Cards.Requests.Revoke
{
    /// <summary>
    /// The optional request body of POST /issuing/cards/{cardId}/revoke. The whole body may be
    /// omitted.
    /// </summary>
    public class RevokeCardRequest
    {
        /// <summary>
        /// The reason why the card is being revoked.
        /// [Optional]
        /// Enum: "expired" "reported_lost" "reported_stolen"
        /// </summary>
        public RevokeReason? Reason { get; set; }
    }
}
