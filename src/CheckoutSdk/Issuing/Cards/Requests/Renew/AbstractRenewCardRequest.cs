using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Renew
{
    public class AbstractRenewCardRequest
    {
        public IssuingCardType? Type { get; set; }
        
        public string DisplayName { get; set; }

        public string Reference { get; set; }
        
        public CardMetadata Metadata { get; set; }
        
        protected AbstractRenewCardRequest(IssuingCardType type)
        {
            Type = type;
        }
    }
}