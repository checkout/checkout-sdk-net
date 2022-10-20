namespace Checkout.Payments
{
    public class PaymentsQueryFilter
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }
    
        public string Reference { get; set; }
    }
}
