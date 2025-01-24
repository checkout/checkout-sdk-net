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
        
        public string RetrievalReferenceNumber { get; set; }
        
        public string PartnerOrderId { get; set; }
        
        public string PartnerStatus { get; set; }
        
        public string PartnerTransactionId { get; set; }
        
        public IList<string> PartnerErrorCodes { get; set; }
        
        public string PartnerErrorMessage { get; set; }
        
        public string PartnerAuthorizationCode { get; set; }
        
        public string PartnerAuthorizationResponseCode { get; set; }
        
        public string FraudStatus { get; set; }
        
        public ProviderAuthorizedPaymentMethod ProviderAuthorizedPaymentMethod { get; set; }
        
        public IList<string> CustomPaymentMethodIds { get; set; }

        public bool? Aft { get; set; }
        
        public string MerchantCategoryCode { get; set; }
        
        public string SchemeMerchantId { get; set; }
        
        public PanProcessedType? PanTypeProcessed { get; set; }
        
        public bool? CkoNetworkTokenAvailable { get; set; }
    }
}