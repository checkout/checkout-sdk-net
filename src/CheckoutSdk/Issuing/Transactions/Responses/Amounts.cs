namespace Checkout.Issuing.Transactions.Responses
{
    public class Amounts
    {
        public TotalHeld TotalHeld { get; set; }
        
        public TotalAuthorized TotalAuthorized { get; set; }
        
        public TotalReversed TotalReversed { get; set; }
        
        public TotalCleared TotalCleared { get; set; }
        
        public TotalRefunded TotalRefunded { get; set; }
    }
}