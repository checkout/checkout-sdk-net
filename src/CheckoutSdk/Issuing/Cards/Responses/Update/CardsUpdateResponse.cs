using Checkout.Issuing.Common.Responses;

namespace Checkout.Issuing.Cards.Responses.Update
{
    /// <summary>
    /// The response to a card update request. This carries the full set of fields returned by
    /// <see cref="AbstractCardResponse"/> (the same shape as the get-card-response), with
    /// <see cref="AbstractCardResponse.LastModifiedDate"/> always populated.
    /// </summary>
    public class CardsUpdateResponse : AbstractCardResponse
    {
        public CardsUpdateResponse() : base(null)
        {
        }

        /// <summary>
        /// Specifies whether the virtual card is set to expire after a single use.
        /// [Optional] Only present when the underlying card is virtual; physical cards never
        /// return this field.
        /// </summary>
        public bool? IsSingleUse { get; set; }
    }
}
