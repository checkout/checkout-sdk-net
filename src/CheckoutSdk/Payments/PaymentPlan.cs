using System;

namespace Checkout.Payments
{
    public class PaymentPlan
    {
        // Recurring
        public AmountVariabilityType? AmountVariability { get; set; }

        // Installment
        public bool? Financing { get; set; }

        public string Amount { get; set; }

        // Common properties
        public int? DaysBetweenPayments { get; set; }

        public int? TotalNumberOfPayments { get; set; }

        public int? CurrentPaymentNumber { get; set; }

        public DateTime Expiry { get; set; }
    }
}