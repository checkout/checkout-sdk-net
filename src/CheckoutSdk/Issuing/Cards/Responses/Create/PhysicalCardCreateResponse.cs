using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// The response returned when a physical card is created (add-physical-card-response).
    /// </summary>
    public class PhysicalCardCreateResponse : AbstractCardCreateResponse
    {
        /// <summary>
        /// Initializes a physical card response.
        /// </summary>
        public PhysicalCardCreateResponse() : base(IssuingCardType.Physical)
        {
        }
    }
}
