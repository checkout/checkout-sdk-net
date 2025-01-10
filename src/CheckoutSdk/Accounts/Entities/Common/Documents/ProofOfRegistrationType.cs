using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum ProofOfRegistrationType
    {
        [EnumMember(Value = "extract_from_trade_register")] ExtractFromTradeRegister,
        [EnumMember(Value = "other")] Other
    }
}