using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class PaymentResponseProcessing
    {
        public string RetrievalReferenceNumber { get; set; }
        public string AcquirerTransactionId { get; set; }
    }
}