using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class PaymentResponseProcessing
    {
        [JsonProperty(PropertyName = "retrieval_reference_number")]
        public string RetrievalReferenceNumber { get; set; }
        
        [JsonProperty(PropertyName = "acquirer_transaction_id")]
        public string AcquirerTransactionId { get; set; }
    }
}