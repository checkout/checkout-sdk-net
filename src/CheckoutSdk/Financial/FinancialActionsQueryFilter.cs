namespace Checkout.Financial
{
    public class FinancialActionsQueryFilter
    {
        public string PaymentId { get; set; }

        public string ActionId { get; set; }
        
        public string Reference { get; set; }

        public int? Limit { get; set; }

        public string PaginationToken { get; set; }
    }
}
