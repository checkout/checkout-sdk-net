namespace Checkout.Metadata.Card
{
    public class AftIndicator
    {
        /// <summary>
        /// Describes whether the card is eligible to take funds from different accounts to fund other non-merchant accounts.
        /// </summary>
        public PullFunds PullFunds { get; set; }
    }
}
