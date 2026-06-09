using System;
using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum AccountAge
    {
        [EnumMember(Value = "no_account")]
        NoAccount,

        [EnumMember(Value = "created_during_transaction")]
        CreatedDuringTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
    }

    public enum ShippingAddressAge
    {
        [EnumMember(Value = "this_transaction")]
        ThisTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
    }

    public enum AccountInfoAuthenticationMethod
    {
        [EnumMember(Value = "no_authentication")]
        NoAuthentication,

        [EnumMember(Value = "own_credentials")]
        OwnCredentials,

        [EnumMember(Value = "federated_id")]
        FederatedId,

        [EnumMember(Value = "issuer_credentials")]
        IssuerCredentials,

        [EnumMember(Value = "third_party_authentication")]
        ThirdPartyAuthentication,

        [EnumMember(Value = "fido")]
        Fido,
    }

    public enum CardholderAccountAgeIndicator
    {
        [EnumMember(Value = "no_account")]
        NoAccount,

        [EnumMember(Value = "this_transaction")]
        ThisTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
    }

    public enum AccountChangeIndicator
    {
        [EnumMember(Value = "this_transaction")]
        ThisTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
    }

    public enum AccountPasswordChangeIndicator
    {
        [EnumMember(Value = "no_change")]
        NoChange,

        [EnumMember(Value = "this_transaction")]
        ThisTransaction,

        [EnumMember(Value = "less_than_thirty_days")]
        LessThanThirtyDays,

        [EnumMember(Value = "thirty_to_sixty_days")]
        ThirtyToSixtyDays,

        [EnumMember(Value = "more_than_sixty_days")]
        MoreThanSixtyDays,
    }

    public enum AccountInfoType
    {
        [EnumMember(Value = "not_applicable")]
        NotApplicable,

        [EnumMember(Value = "credit")]
        Credit,

        [EnumMember(Value = "debit")]
        Debit,
    }

    /// <summary>
    /// Additional information about the cardholder's account.
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// The number of purchases with this cardholder's account during the last six months.
        /// [Optional]
        /// min 0
        /// max 9999
        /// </summary>
        public int? PurchaseCount { get; set; }

        /// <summary>
        /// The length of time that the payment account was enrolled in the cardholder's account.
        /// [Optional]
        /// </summary>
        public AccountAge? AccountAge { get; set; }

        /// <summary>
        /// The number of Add Card attempts in the last 24 hours.
        /// [Optional]
        /// min 0
        /// max 999
        /// </summary>
        public int? AddCardAttempts { get; set; }

        /// <summary>
        /// Indicates when the shipping address for this transaction was first used.
        /// [Optional]
        /// </summary>
        public ShippingAddressAge? ShippingAddressAge { get; set; }

        /// <summary>
        /// Indicates if the cardholder name on the account is identical to the shipping name used for this transaction.
        /// [Optional]
        /// </summary>
        public bool? AccountNameMatchesShippingName { get; set; }

        /// <summary>
        /// Indicates whether suspicious activity on the cardholder account has been observed.
        /// [Optional]
        /// </summary>
        public bool? SuspiciousAccountActivity { get; set; }

        /// <summary>
        /// The number of transactions (successful and abandoned) for the cardholder account across
        /// all payment accounts in the previous 24 hours.
        /// [Optional]
        /// min 0
        /// max 999
        /// </summary>
        public int? TransactionsToday { get; set; }

        /// <summary>
        /// Information about how the cardholder was authenticated before or during the transaction.
        /// [Optional]
        /// </summary>
        public AccountInfoAuthenticationMethod? AuthenticationMethod { get; set; }

        /// <summary>
        /// The length of time the cardholder has held the account with the 3DS Requestor.
        /// [Optional]
        /// </summary>
        public CardholderAccountAgeIndicator? CardholderAccountAgeIndicator { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder's account with the 3DS Requestor was last changed, in ISO 8601 format.
        /// Changes include: updating billing or shipping address, adding a new payment account or user.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? AccountChange { get; set; }

        /// <summary>
        /// The amount of time since the cardholder's account information with the 3DS Requestor was last changed.
        /// [Optional]
        /// </summary>
        public AccountChangeIndicator? AccountChangeIndicator { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder opened the account with the 3DS Requestor, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? AccountDate { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder's account with the 3DS Requestor was last reset
        /// or had a password change, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? AccountPasswordChange { get; set; }

        /// <summary>
        /// The amount of time since the cardholder's account with the 3DS Requestor was last reset
        /// or had a password change.
        /// [Optional]
        /// </summary>
        public AccountPasswordChangeIndicator? AccountPasswordChangeIndicator { get; set; }

        /// <summary>
        /// The number of transactions associated with the cardholder's account in the previous year.
        /// Set to 999 if the number of transactions is 1000 or greater.
        /// [Optional]
        /// max 999
        /// </summary>
        public int? TransactionsPerYear { get; set; }

        /// <summary>
        /// The UTC date and time the payment account was enrolled in the cardholder's account with
        /// the 3DS Requestor, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? PaymentAccountAge { get; set; }

        /// <summary>
        /// The UTC date and time the shipping address used for the transaction was first used with
        /// the 3DS Requestor, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? ShippingAddressUsage { get; set; }

        /// <summary>
        /// The type of account, in the case of a card product with multiple accounts.
        /// [Optional]
        /// </summary>
        public AccountInfoType? AccountType { get; set; }

        /// <summary>
        /// Additional information about the account optionally provided by the 3DS Requestor.
        /// [Optional]
        /// max 64 characters
        /// </summary>
        public string AccountId { get; set; }
    }
}
