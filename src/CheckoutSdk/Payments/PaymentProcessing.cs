using System;

namespace Checkout.Payments
{
    public sealed class PaymentProcessing 
    {
        public string RetrievalReferenceNumber { get; set; }

        public string AcquirerTransactionId { get; set; }

        public string RecommendationCode { get; set; }
              
    }
}