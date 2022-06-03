namespace Checkout.Sessions
{
    public class CardholderAccountInfo
    {
        public long? PurchaseCount { get; set; }

        public string AccountAge { get; set; }

        public long? AddCardAttempts { get; set; }

        public string ShippingAddressAge { get; set; }

        public bool? AccountNameMatchesShippingName { get; set; }

        public bool? SuspiciousAccountActivity { get; set; }

        public long? TransactionsToday { get; set; }

        public AuthenticationMethod? AuthenticationMethod { get; set; }
    }
}