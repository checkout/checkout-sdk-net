using System;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.
    Customer.
    Summary
{
    /// <summary>
    /// summary
    /// Summary of the customer's transaction history.  Used for risk assessment when source.type is Tamara
    /// </summary>
    public class Summary
    {
        /// <summary>
        /// The date the customer registered.
        /// [Optional]
        /// <date>
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// The date of the customer's first transaction.
        /// [Optional]
        /// <date>
        /// </summary>
        public DateTime? FirstTransactionDate { get; set; }

        /// <summary>
        /// The date of the customer's last payment.
        /// [Optional]
        /// <date>
        /// </summary>
        public DateTime? LastPaymentDate { get; set; }

        /// <summary>
        /// The total number of orders made by the customer.
        /// [Optional]
        /// </summary>
        public int? TotalOrderCount { get; set; }

        /// <summary>
        /// The amount of the customer's last payment.
        /// [Optional]
        /// </summary>
        public long? LastPaymentAmount { get; set; }

        /// <summary>
        /// Specifies whether the customer is a premium customer.
        /// [Optional]
        /// </summary>
        public bool? IsPremiumCustomer { get; set; }

        /// <summary>
        /// Specifies whether the customer is a returning customer.
        /// [Optional]
        /// </summary>
        public bool? IsReturningCustomer { get; set; }

        /// <summary>
        /// The customer's lifetime value. This is the total monetary amount that the customer has ordered, in their
        /// local currency, excluding the following:
        /// canceled orders
        /// rejected payments
        /// refunded payments
        /// Tamara payments
        /// The lifetime value is an indicator of how valuable the relationship with the customer is to your company.
        /// [Optional]
        /// </summary>
        public long? LifetimeValue { get; set; }
    }
}