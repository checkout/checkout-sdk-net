using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The type of payment instrument.
    /// </summary>
    public enum InstrumentType
    {
        [EnumMember(Value = "bank_account")]
        BankAccount,

        [EnumMember(Value = "token")]
        Token,

        [EnumMember(Value = "card")]
        Card,

        /// <summary>
        /// Retained for the previous-platform instruments API. Not a value of the current
        /// platform's instrument type.
        /// </summary>
        [EnumMember(Value = "card_token")]
        CardToken,

        [EnumMember(Value = "sepa")]
        Sepa,

        [EnumMember(Value = "ach")]
        Ach,

        [EnumMember(Value = "bacs")]
        Bacs,
    }
}
