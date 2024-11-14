using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Response.Source
{
    public class CardResponseSource : AbstractResponseSource, IResponseSource
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Scheme { get; set; }
        
        [Obsolete("This property will be removed in the future, and should not be used. Use LocalSchemes instead.", false)]
        public string SchemeLocal { get; set; }
        
        
        public IList<string> LocalSchemes { get; set; }

        public string Last4 { get; set; }

        public string Fingerprint { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }
        
        public CardWalletType? CardWalletType { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public string AvsCheck { get; set; }

        public string CvvCheck { get; set; }

        public string PaymentAccountReference { get; set; }

        public string EncryptedCardNumber { get; set; }

        public AccountUpdateStatus? AccountUpdateStatus { get; set; }
        
        public AccountHolderResponse AccountHolder { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
       
    }
}