using System;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsCustomerSummary
    {
        public DateTime? RegistrationDate { get; set; }
        
        public DateTime? FirstTransactionDate { get; set; }
        
        public DateTime? LastPaymentDate { get; set; }
        
        public long? TotalOrderCount { get; set; }
        
        public double? LastPaymentAmount { get; set; }
    }
}