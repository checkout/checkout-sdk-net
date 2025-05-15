using Checkout.Issuing.Common.Responses;
using System.Collections.Generic;

namespace Checkout.Issuing.Cardholders.Responses
{
    public class CardholderCardsResponse : HttpMetadata
    {
        public IList<AbstractCardResponse> Cards { get; set; }
    }
}