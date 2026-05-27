using System;

namespace Checkout.Payments
{
    public class PaymentPlan
    {
        // Recurring
        public AmountVariabilityType? AmountVariability { get; set; }

        // Installment
        public bool? Financing { get; set; }

        /// <summary>
        /// The amount to charge for each payment in the plan, in the minor currency unit.
        /// Required when source.type is blik, payment_plan.amount_variability is Fixed,
        /// and the recurring agreement is created without an initial payment (amount set to 0).
        /// [Optional]
        /// &gt;= 1
        /// </summary>
        public long? Amount { get; set; }

        // Common properties
        public int? DaysBetweenPayments { get; set; }

        public int? TotalNumberOfPayments { get; set; }

        public int? CurrentPaymentNumber { get; set; }

        /// <summary>
        /// The date after which no further payments will be performed in the format YYYYMMDD
        /// </summary>
        public string Expiry { get; set; }

        /// <summary>
        /// The name of the payment plan. Required when source.type is blik.
        /// For Blik merchant-initiated requests using an external partner_agreement_id,
        /// this value is used as the Blik Alias Label.
        /// [Optional]
        /// &lt;= 35 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date on which the first payment will be taken, in YYYYMMDD format.
        /// Required when source.type is blik and the recurring agreement is created
        /// without an initial payment (amount set to 0).
        /// [Optional]
        /// </summary>
        public string StartDate { get; set; }
    }
}