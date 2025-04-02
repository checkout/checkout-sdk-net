namespace Checkout.Issuing.Transactions.Responses
{
    public class ReferenceTransaction
    {
        public string TransactionId { get; set; }
        
        public ReferenceType? ReferenceType { get; set; }
    }
}