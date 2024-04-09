using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    public enum InstrumentType
    {
        [EnumMember(Value = "bank_account")] BankAccount,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "card_token")] CardToken,
        [EnumMember(Value = "sepa")] Sepa,
    }
}