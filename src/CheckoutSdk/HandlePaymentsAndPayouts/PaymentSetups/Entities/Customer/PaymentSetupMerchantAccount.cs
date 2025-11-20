namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupMerchantAccount
    {
        public string Id { get; set; }

        public string RegistrationDate { get; set; }

        public string LastModified { get; set; }

        public bool? ReturningCustomer { get; set; }

        public string FirstTransactionDate { get; set; }

        public string LastTransactionDate { get; set; }

        public int? TotalOrderCount { get; set; }

        public long? LastPaymentAmount { get; set; }
    }
}