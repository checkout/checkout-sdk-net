using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsCustomerSummary
    {
        /// <summary>
        /// The date the customer registered.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// The date of the customer's first transaction.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? FirstTransactionDate { get; set; }

        /// <summary>
        /// The date of the customer's last payment.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? LastPaymentDate { get; set; }

        /// <summary>
        /// The total number of orders made by the customer.
        /// [Optional]
        /// </summary>
        public long? TotalOrderCount { get; set; }

        /// <summary>
        /// The amount of the customer's last payment.
        /// [Optional]
        /// </summary>
        public double? LastPaymentAmount { get; set; }

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
        /// The customer's lifetime value. This is the total monetary amount that the customer has
        /// ordered, in their local currency, excluding canceled orders, rejected payments,
        /// refunded payments and Tamara payments.
        /// The lifetime value is an indicator of how valuable the relationship with the customer
        /// is to your company.
        /// [Optional]
        /// </summary>
        public double? LifetimeValue { get; set; }
    }
}
