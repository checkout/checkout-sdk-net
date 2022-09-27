namespace Checkout.Payments
{
    public class BillingPlan
    {
        public BillingPlanType? Type { get; set; }
        
        public bool? SkipShippingAddress { get; set; }
        
        public bool? ImmutableShippingAddress { get; set; }
    }
}