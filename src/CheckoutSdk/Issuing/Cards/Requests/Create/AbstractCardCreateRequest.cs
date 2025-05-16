using Checkout.Issuing.Common;
using System;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public abstract class AbstractCardCreateRequest
    {
        public IssuingCardType? Type { get; set; }

        public string CardholderId { get; set; }
        
        public string CardProductId { get; set; }

        public CardLifetime Lifetime { get; set; }

        public string Reference { get; set; }
        
        public CardMetadata Metadata { get; set; }
        
        public DateTime? RevocationDate { get; set; }

        public string DisplayName { get; set; }

        public bool ActivateCard { get; set; }

        protected AbstractCardCreateRequest(IssuingCardType type)
        {
            Type = type;
        }
    }
}