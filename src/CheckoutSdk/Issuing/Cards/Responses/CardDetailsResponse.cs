using Checkout.Common;
using System;

namespace Checkout.Issuing.Cards.Responses
{
    public abstract class CardDetailsResponse : Resource
    {
        public CardType? Type { get; set; }

        public string Id { get; set; }

        public string CardholderId { get; set; }

        public string CardProductId { get; set; }

        public string ClientId { get; set; }

        public string LastFour { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public CardStatus? Status { get; set; }

        public string DisplayName { get; set; }

        public Currency? BillingCurrency { get; set; }

        public CountryCode? IssuingCountry { get; set; }

        public string Reference { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        protected CardDetailsResponse(CardType? type)
        {
            Type = type;
        }
    }
}