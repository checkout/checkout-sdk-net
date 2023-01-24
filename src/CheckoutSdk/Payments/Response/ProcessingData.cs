using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Response
{
    public class ProcessingData
    {
        public PreferredSchema? PreferredScheme { get; set; }
        
        public string AppId { get; set; }
        
        public string PartnerCustomerId { get; set; }
        
        public string PartnerPaymentId { get; set; }
        
        public long? TaxAmount { get; set; }
        
        public CountryCode? PurchaseCountry { get; set; }
        
        public string Locale { get; set; }
        
        public string PartnerOrderId { get; set; }
        
        public string FraudStatus { get; set; }
        
        public ProviderAuthorizedPaymentMethod ProviderAuthorizedPaymentMethod { get; set; }
        
        public IList<string> CustomPaymentMethodIds { get; set; }
        
        public IList<string> PartnerErrorCodes { get; set; }
        
        public string PartnerReason { get; set; }
        
    }
}