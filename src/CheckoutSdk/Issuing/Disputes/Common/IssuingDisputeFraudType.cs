using System.Runtime.Serialization;

namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// The type of fraud the cardholder is asserting.
    /// [Beta]
    /// </summary>
    public enum IssuingDisputeFraudType
    {
        /// <summary>
        /// The cardholder does not have the physical card and cannot explain how it was lost.
        /// </summary>
        [EnumMember(Value = "card_lost")] CardLost,

        /// <summary>
        /// The cardholder does not have the physical card and provided an explanation as to how it was lost.
        /// For example, their wallet was stolen.
        /// </summary>
        [EnumMember(Value = "card_stolen")] CardStolen,

        /// <summary>
        /// The card was issued and mailed but was not received by the cardholder.
        /// </summary>
        [EnumMember(Value = "card_never_received")] CardNeverReceived,

        /// <summary>
        /// The account was opened fraudulently using a stolen, fake, or synthetic identity.
        /// </summary>
        [EnumMember(Value = "fraudulent_account")] FraudulentAccount,

        /// <summary>
        /// The cardholder has their physical card but a duplicate or altered card was used in an
        /// unauthorized card-present transaction.
        /// </summary>
        [EnumMember(Value = "counterfeit_card")] CounterfeitCard,

        /// <summary>
        /// A fraudster gained control of the cardholder's account or credentials. For example, they may have
        /// changed the registered address, ordered a new card, or provisioned the card to a digital wallet.
        /// </summary>
        [EnumMember(Value = "account_takeover")] AccountTakeover,

        /// <summary>
        /// The cardholder still has the card but the card details were used in an unauthorized
        /// card-not-present transaction. For example, it was used in an ecommerce transaction, or in a
        /// mail order/telephone order (MOTO) payment.
        /// </summary>
        [EnumMember(Value = "card_not_present_fraud")] CardNotPresentFraud,

        /// <summary>
        /// The merchant deliberately misled the cardholder. For example, the goods provided were
        /// sub-standard, there were hidden charges, or the services advertised were fake.
        /// </summary>
        [EnumMember(Value = "merchant_misrepresentation")] MerchantMisrepresentation,

        /// <summary>
        /// The cardholder was tricked into authorizing the transaction themselves. For example, due to a
        /// scam, an impersonation, or authorized push payment (APP) fraud.
        /// </summary>
        [EnumMember(Value = "cardholder_manipulation")] CardholderManipulation,

        /// <summary>
        /// A security check failed and made fraudulent activity possible. For example, a card verification
        /// value (CVV) check was not performed, or the EMV cryptogram was not validated.
        /// </summary>
        [EnumMember(Value = "incorrect_processing")] IncorrectProcessing,

        /// <summary>
        /// The confirmed fraud does not fit any of the other fraud type categories, or there were multiple
        /// fraud types applicable to the same transaction.
        /// </summary>
        [EnumMember(Value = "other")] Other
    }
}
