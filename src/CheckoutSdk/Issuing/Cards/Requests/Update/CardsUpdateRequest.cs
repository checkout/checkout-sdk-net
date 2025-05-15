using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Update
{
    public class CardsUpdateRequest
    {
        public string Reference { get; set; }
        
        public CardMetadata Metadata { get; set; }
        
        public int? ExpiryMonth { get; set; }
        
        public int? ExpiryYear { get; set; }
    }
}