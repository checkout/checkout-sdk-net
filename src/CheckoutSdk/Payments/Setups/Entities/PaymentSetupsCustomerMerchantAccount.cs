namespace Checkout.Payments.Setups.Requests.Entities
{
    public class PaymentSetupsCustomerMerchantAccount
    {
        public string Id { get; set; }

        public string RegistrationDate { get; set; }

        public string LastModified { get; set; }

        public bool? ReturningCustomer { get; set; }

        public string FirstTransactionDate { get; set; }

        public string LastTransactionDate { get; set; }

        public int? TotalOrderCount { get; set; }

        public double? LastPaymentAmount { get; set; }
    }
}