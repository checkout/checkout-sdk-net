namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupMerchantAccount
    {
        /// <summary>
        /// The unique identifier of the merchant account
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date when the customer's account was first registered with the merchant
        /// </summary>
        public string RegistrationDate { get; set; }

        /// <summary>
        /// The date when the customer's account was last modified
        /// </summary>
        public string LastModified { get; set; }

        /// <summary>
        /// Indicates whether this is a returning customer
        /// </summary>
        public bool? ReturningCustomer { get; set; }

        /// <summary>
        /// The date of the customer's first transaction with the merchant
        /// </summary>
        public string FirstTransactionDate { get; set; }

        /// <summary>
        /// The date of the customer's most recent transaction with the merchant
        /// </summary>
        public string LastTransactionDate { get; set; }

        /// <summary>
        /// The total number of orders the customer has placed with the merchant
        /// </summary>
        public int? TotalOrderCount { get; set; }

        /// <summary>
        /// The amount of the customer's last payment with the merchant
        /// </summary>
        public long? LastPaymentAmount { get; set; }
    }
}