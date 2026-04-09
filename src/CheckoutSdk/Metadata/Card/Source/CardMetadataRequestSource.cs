namespace Checkout.Metadata.Card.Source
{
    public abstract class CardMetadataRequestSource
    {
        /// <summary>
        /// The source type.
        /// [Required] Values: <c>card</c>, <c>bin</c>, <c>token</c>, <c>id</c>.
        /// </summary>
        public CardMetadataSourceType? Type { get; set; }

        protected CardMetadataRequestSource(CardMetadataSourceType type)
        {
            Type = type;
        }
    }
}
