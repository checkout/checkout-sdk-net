namespace Checkout.Payments
{
    public class PaymentProcessing
    {
        public string RetrievalReferenceNumber { get; set; }

        public string AcquirerTransactionId { get; set; }

        public string RecommendationCode { get; set; }
        
        public string PartnerOrderId { get; set; }
        
        public string PartnerSessionId { get; set; }
        
        public string PartnerClientToken { get; set; }
        
        public string PartnerPaymentId { get; set; }

        public string ContinuationPayload { get; set; }
        
        public string Pun { get; set; }
    }
}