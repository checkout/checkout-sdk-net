using Checkout.Issuing.Cards;
using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Issuing.Cardholders
{
    public class CardholderCardsResponse : Resource
    {
        public IList<CardResponse> Cards { get; set; }
    }
}