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
    }
}
