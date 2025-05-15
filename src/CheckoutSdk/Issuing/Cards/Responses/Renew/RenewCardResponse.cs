using Checkout.Common;
using Checkout.Issuing.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Renew
{
    public class RenewCardResponse : Resource
    {
        public string ParentCardId { get; set; }
        
        public string CardholderId { get; set; }
        
        public CardStatus? Status { get; set; }
        
        public IssuingCardType? Type { get; set; }

        public string Id { get; set; }

        public string ClientId { get; set; }
        
        public string EntityId { get; set; }
        
        public string LastFour { get; set; }
        
        public int? ExpiryYear { get; set; }
        
        public int? ExpiryMonth { get; set; }
        
        public string DisplayName { get; set; }
        
        public string Reference { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public Currency? BillingCurrency { get; set; }
        
        public CountryCode? IssuingCountry { get; set; }
    }
}