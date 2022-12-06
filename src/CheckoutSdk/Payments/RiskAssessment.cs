namespace Checkout.Payments
{
    public class RiskAssessment
    {
        public bool? Flagged { get; set; }
        
        public long Score { get; set; }
    }
}