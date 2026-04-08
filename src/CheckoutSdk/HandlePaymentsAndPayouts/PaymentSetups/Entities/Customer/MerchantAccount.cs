namespace Checkout.Payments.Setups.Entities
{
    public class MerchantAccount
    {
        /// <summary>
        /// The merchant's unique identifier for the customer's account
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date the customer registered their account with the merchant (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string RegistrationDate { get; set; }

        /// <summary>
        /// The date the customer's account with the merchant was last modified (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string LastModified { get; set; }

        /// <summary>
        /// Specifies if the customer is a returning customer
        /// [Optional]
        /// </summary>
        public bool? ReturningCustomer { get; set; }

        /// <summary>
        /// The date of the customer's first transaction (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string FirstTransactionDate { get; set; }

        /// <summary>
        /// The date of the customer's most recent transaction (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string LastTransactionDate { get; set; }

        /// <summary>
        /// The total number of orders made by the customer
        /// [Optional]
        /// </summary>
        public int? TotalOrderCount { get; set; }

        /// <summary>
        /// The payment amount of the customer's most recent transaction
        /// [Optional]
        /// </summary>
        public decimal? LastPaymentAmount { get; set; }
    }
}
