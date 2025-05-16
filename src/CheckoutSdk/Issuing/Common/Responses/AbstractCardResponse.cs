using Checkout.Common;
using System;

namespace Checkout.Issuing.Common.Responses
{
    public abstract class AbstractCardResponse : Resource
    {
        public IssuingCardType? Type { get; set; }

        public string Id { get; set; }

        public string ClientId { get; set; }
        
        public string EntityId { get; set; }
        
        public string CardholderId { get; set; }
        
        public string CardProductId { get; set; }

        public string LastFour { get; set; }
        
        public int? ExpiryMonth { get; set; }
        
        public int? ExpiryYear { get; set; }
        
        public CardStatus? Status { get; set; }
        
        public Currency? BillingCurrency { get; set; }
        
        public CountryCode? IssuingCountry { get; set; }
        
        public string DisplayName { get; set; }
        
        public string Reference { get; set; }
        
        public CardMetadata Metadata { get; set; }
        
        public string RevocationDate { get; set; }
        
        public string RootCardId { get; set; }
        
        public string ParentCardId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? LastModifiedDate { get; set; }

        protected AbstractCardResponse(IssuingCardType? type)
        {
            Type = type;
        }
    }
}