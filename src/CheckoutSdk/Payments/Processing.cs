namespace Checkout.Payments
{
    public sealed class Processing 
    {
        public string AcquirerReferenceNumber { get; set; }

        public string RetrievalReferenceNumber { get; set; }

        public string AcquirerTransactionId { get; set; }
             
    }
}