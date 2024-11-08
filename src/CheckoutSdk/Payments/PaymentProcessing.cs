using System.Collections.Generic;

namespace Checkout.Payments
{
    public class PaymentProcessing
    {
        public string RetrievalReferenceNumber { get; set; }

        public string AcquirerTransactionId { get; set; }

        public string RecommendationCode { get; set; }

        public string Scheme { get; set; }

        public string PartnerMerchantAdviceCode { get; set; }

        public string PartnerResponseCode { get; set; }
        
        public string PartnerOrderId { get; set; }
        
        public string PartnerSessionId { get; set; }
        
        public string PartnerClientToken { get; set; }
        
        public string PartnerPaymentId { get; set; }
        
        public PanProcessedType? PanTypeProcessed { get; set; }

        public string ContinuationPayload { get; set; }
        
        public string Pun { get; set; }
        
        public string PartnerStatus { get; set; }
        
        public string PartnerTransactionId { get; set; }
        
        public IList<string> PartnerErrorCodes { get; set; }
        
        public string PartnerErrorMessage { get; set; }
        
        public string PartnerAuthorizationCode { get; set; }
        
        public string PartnerAuthorizationResponseCode { get; set; }

        public long? SurchargeAmount { get; set; }
        
        public bool? CkoNetworkTokenAvailable { get; set; }
        
        public string MerchantCategoryCode { get; set; }

        public long? SchemeMerchantId { get; set; }

        public bool? Aft { get; set; }

    }
}