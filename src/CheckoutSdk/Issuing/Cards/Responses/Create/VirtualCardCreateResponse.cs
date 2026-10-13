using Checkout.Issuing.Common;
using System.Collections.Generic;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// The response returned when a virtual card is created (add-virtual-card-response).
    /// </summary>
    public class VirtualCardCreateResponse : AbstractCardCreateResponse
    {
        /// <summary>
        /// Initializes a virtual card response.
        /// </summary>
        public VirtualCardCreateResponse() : base(IssuingCardType.Virtual)
        {
        }

        /// <summary>
        /// The card's credentials.
        /// [Optional]
        /// </summary>
        public Credentials Credentials { get; set; }

        /// <summary>
        /// The controls that were set on the card.
        /// [Optional]
        /// </summary>
        public IList<AbstractCardControlsResponse> Controls { get; set; }
    }
}
