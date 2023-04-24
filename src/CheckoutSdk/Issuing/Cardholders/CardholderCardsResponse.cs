using Checkout.Issuing.Cards;
using System.Collections.Generic;

namespace Checkout.Issuing.Cardholders
{
    public class CardholderCardsResponse : HttpMetadata
    {
        public IList<CardResponse> Cards { get; set; }
    }
}