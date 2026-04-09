using Checkout.Metadata.Card.Source;

namespace Checkout.Metadata.Card
{
    public class CardMetadataRequest
    {
        /// <summary>
        /// The card source to retrieve metadata for (card number, BIN, token, or instrument ID).
        /// [Required]
        /// </summary>
        public CardMetadataRequestSource Source { get; set; }

        /// <summary>
        /// The format to provide the output in. A <c>basic</c> response includes standard metadata only,
        /// while <c>card_payouts</c> also includes fields specific to card payouts.
        /// [Optional] Default: <c>basic</c>. Values: <c>basic</c>, <c>card_payouts</c>.
        /// </summary>
        public CardMetadataFormatType? Format { get; set; }

        /// <summary>
        /// A reference you can later use to identify this request. For example, an order number.
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }
    }
}
