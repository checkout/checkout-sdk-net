using System;

namespace Checkout.Payments.Setups.Entities
{
    public class MerchantAccount
    {
        /// <summary>
        /// The merchant's unique identifier for the customer's account
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date the customer registered their account with the merchant
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// The date the customer's account with the merchant was last modified
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Specifies if the customer is a returning customer
        /// </summary>
        public bool? ReturningCustomer { get; set; }

        /// <summary>
        /// The date of the customer's first transaction
        /// </summary>
        public DateTime? FirstTransactionDate { get; set; }

        /// <summary>
        /// The date of the customer's most recent transaction
        /// </summary>
        public DateTime? LastTransactionDate { get; set; }

        /// <summary>
        /// The total number of orders made by the customer
        /// </summary>
        public int? TotalOrderCount { get; set; }

        /// <summary>
        /// The payment amount of the customer's most recent transaction
        /// </summary>
        public long? LastPaymentAmount { get; set; }
    }
}