using Checkout.Common;
using System;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Customer
    {
        /// <summary>
        /// The customer's email address. Required if source.type is tamara.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer's name. Required if source.type is tamara.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The unique identifier for an existing customer.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The customer's phone number. Required if source.type is tamara.
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The customer's value-added tax (VAT) registration number.
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Summary of the customer's transaction history. Used for risk assessment when source.type is Tamara
        /// </summary>
        public CustomerSummary Summary { get; set; }
    }

    public class CustomerSummary
    {
        /// <summary>
        /// The date the customer registered.
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// The date of the customer's first transaction.
        /// </summary>
        public DateTime? FirstTransactionDate { get; set; }

        /// <summary>
        /// The date of the customer's last payment.
        /// </summary>
        public DateTime? LastPaymentDate { get; set; }

        /// <summary>
        /// The total number of orders made by the customer.
        /// </summary>
        public int? TotalOrderCount { get; set; }

        /// <summary>
        /// The amount of the customer's last payment.
        /// </summary>
        public decimal? LastPaymentAmount { get; set; }

        /// <summary>
        /// Specifies whether the customer is a premium customer.
        /// </summary>
        public bool? IsPremiumCustomer { get; set; }

        /// <summary>
        /// Specifies whether the customer is a returning customer.
        /// </summary>
        public bool? IsReturningCustomer { get; set; }

        /// <summary>
        /// The customer's lifetime value. This is the total monetary amount that the customer has ordered, 
        /// in their local currency, excluding the following:
        /// - canceled orders
        /// - rejected payments
        /// - refunded payments
        /// - Tamara payments
        /// The lifetime value is an indicator of how valuable the relationship with the customer is to your company.
        /// </summary>
        public decimal? LifetimeValue { get; set; }
    }
}