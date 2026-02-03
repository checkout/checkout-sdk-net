using System;

namespace Checkout.Authentication.Standalone.Common.AccountInfo
{
    /// <summary>
    /// account_info
    /// Additional information about the Cardholder's account.
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// The number of purchases with this cardholder's account during the last six months.
        /// [Optional]
        /// [ 0 .. 9999 ]
        /// </summary>
        public long? PurchaseCount { get; set; }

        /// <summary>
        /// The length of time that the payment account was enrolled in the cardholder's account.
        /// [Optional]
        /// </summary>
        public AccountAgeType? AccountAge { get; set; }

        /// <summary>
        /// The number of Add Card attempts in the last 24 hours.
        /// [Optional]
        /// [ 0 .. 999 ]
        /// </summary>
        public long? AddCardAttempts { get; set; }

        /// <summary>
        /// Indicates when the shipping address used for this transaction was first used.
        /// [Optional]
        /// </summary>
        public ShippingAddressAgeType? ShippingAddressAge { get; set; }

        /// <summary>
        /// Indicates if the Cardholder Name on the account is identical to the shipping Name used for this transaction.
        /// [Optional]
        /// </summary>
        public bool? AccountNameMatchesShippingName { get; set; }

        /// <summary>
        /// Indicates whether suspicious activity on the cardholder account has been observed.
        /// [Optional]
        /// </summary>
        public bool? SuspiciousAccountActivity { get; set; }

        /// <summary>
        /// The number of transactions (successful and abandoned) for the cardholder account across all payment accounts
        /// in the previous 24 hours
        /// [Optional]
        /// [ 0 .. 999 ]
        /// </summary>
        public long? TransactionsToday { get; set; }

        /// <summary>
        /// [DEPRECATED]
        ///  Information about how the cardholder was authenticated before or during the transaction.
        /// [Optional]
        /// </summary>
        [Obsolete("This property will be removed in the future, and should not be used.")]
        public AuthenticationMethodType? AuthenticationMethod { get; set; }

        /// <summary>
        /// The length of time the cardholder has held the account with the 3DS Requestor.
        /// [Optional]
        /// </summary>
        public CardholderAccountAgeIndicatorType? CardholderAccountAgeIndicator { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder’s account with the 3DS Requestor was last changed, in ISO 8601 format.
        /// Changes that affect this value include:
        /// updating the billing or shipping address
        /// adding a new payment account
        /// adding a new user
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? AccountChange { get; set; }

        /// <summary>
        /// The amount of time since the cardholder’s account information with the 3DS Requestor was last changed.
        /// Changes that affect this value include:
        /// updating the billing or shipping address
        /// adding a new payment account
        /// adding a new user
        /// [Optional]
        /// </summary>
        public AccountChangeIndicatorType? AccountChangeIndicator { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder opened the account with the 3DS Requestor, in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? AccountDate { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder’s account with the 3DS Requestor was last reset or had a password
        /// change, in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? AccountPasswordChange { get; set; }

        /// <summary>
        /// The amount of time since the cardholder’s account with the 3DS Requestor was last reset or had a password
        /// change.
        /// [Optional]
        /// </summary>
        public AccountPasswordChangeIndicatorType? AccountPasswordChangeIndicator { get; set; }

        /// <summary>
        /// The number of transactions associated with the cardholder's account with the 3DS Requestor in the previous
        /// year. This value includes both successful and abandoned transactions, across all payment accounts.
        /// Set this value to 999 if the number of transactions is 1000 or greater.
        /// [Optional]
        /// &lt;= 3
        /// &lt;= 999
        /// </summary>
        public long? TransactionsPerYear { get; set; }

        /// <summary>
        /// The UTC date and time the payment account was enrolled in the cardholder’s account with the 3DS Requestor,
        /// in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? PaymentAccountAge { get; set; }

        /// <summary>
        /// The UTC date and time the shipping address used for the transaction was first used with the 3DS Requestor,
        /// in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? ShippingAddressUsage { get; set; }

        /// <summary>
        /// The type of account, in the case of a card product with multiple accounts.
        /// [Optional]
        /// </summary>
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// Additional information about the account optionally provided by the 3DS Requestor.
        /// [Optional]
        /// &lt;= 64
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Information about how the 3DS Requestor authenticated the cardholder before or during the transaction.
        /// [Optional]
        /// </summary>
        public ThreeDsRequestorAuthenticationInfo.ThreeDsRequestorAuthenticationInfo ThreeDsRequestorAuthenticationInfo { get; set; }
    }
}