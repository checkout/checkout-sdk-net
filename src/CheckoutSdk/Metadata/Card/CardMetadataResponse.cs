using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Metadata.Card
{
    public class CardMetadataResponse : HttpMetadata
    {
        public string Bin { get; set; }

        public string Scheme { get; set; }

        [Obsolete("This property will be removed in the future, and should not be used. Use LocalSchemes instead.", false)]
        public SchemeLocalType? SchemeLocal { get; set; }

        public IList<SchemeLocalType> LocalSchemes { get; set; }

        public CardMetadataType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public Currency? Currency { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string IssuerCountryName { get; set; }

        public string ProductId { get; set; }
        
        public string ProductType { get; set; }
        
        public string SubproductId { get; set; }

        public bool? RegulatedIndicator { get; set; }
        
        public string RegulatedType { get; set; }
        
        public CardMetadataPayouts CardPayouts { get; set; }

        public SchemeMetadata SchemeMetadata { get; set; }

        public AccountFundingTransaction AccountFundingTransaction { get; set; }
    }
}