namespace Checkout.Issuing.Cards.Requests.Revoke
{
    /// <summary>
    /// The optional request body; the whole body may be omitted.
    /// </summary>
    public class RevokeCardRequest
    {
        /// <summary>
        /// The reason for revoking the card.
        /// [Optional]
        /// </summary>
        public RevokeReason? Reason { get; set; }
    }
}