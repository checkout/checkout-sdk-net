using Checkout.Common;
using System;

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

        [Obsolete("This property will be removed in the future, and should not be used.")]
        public AuthenticationMethod? AuthenticationMethod { get; set; }

        public CardholderAccountAgeIndicatorType? CardholderAccountAgeIndicator { get; set; }

        public DateTime? AccountChange { get; set; }

        public AccountChangeIndicatorType? AccountChangeIndicator { get; set; }

        public DateTime? AccountDate { get; set; }

        public string AccountPasswordChange { get; set; }

        public AccountPasswordChangeIndicatorType? AccountPasswordChangeIndicator { get; set; }

        public int? TransactionsPerYear { get; set; }

        public DateTime? PaymentAccountAge { get; set; }

        public DateTime? ShippingAddressUsage { get; set; }

        public AccountTypeCardProductType? AccountType { get; set; }

        public string AccountId { get; set; }

        public ThreeDsRequestorAuthenticationInfo ThreeDsRequestorAuthenticationInfo { get; set; }
    }
}