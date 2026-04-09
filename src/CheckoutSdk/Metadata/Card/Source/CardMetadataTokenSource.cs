namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataTokenSource : CardMetadataRequestSource
    {
        public CardMetadataTokenSource() : base(CardMetadataSourceType.Token)
        {
        }

        /// <summary>
        /// The Checkout.com unique token generated when the card's details were tokenized.
        /// [Required] Pattern: <c>tok_</c> followed by 26 alphanumeric characters.
        /// </summary>
        public string Token { get; set; }
    }
}
