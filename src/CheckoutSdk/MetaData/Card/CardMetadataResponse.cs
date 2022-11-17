﻿using Checkout.Common;

namespace Checkout.Metadata.Card
{
    public class CardMetadataResponse : HttpMetadata
    {
        public string Bin { get; set; }

        public string Scheme { get; set; }

        public SchemeLocalType? SchemeLocal { get; set; }
        
        public CardType? CardType { get; set; }
        
        public CardCategory? CardCategory { get; set; }
        
        public string Issuer { get; set; }
        
        public CountryCode? IssuerCountry { get; set; }
        
        public string IssuerCountryName { get; set; }
        
        public string ProductId { get; set; }
        
        public string ProductType { get; set; }
        
        public CardMetadataPayouts Payouts { get; set; }
        
    }
}