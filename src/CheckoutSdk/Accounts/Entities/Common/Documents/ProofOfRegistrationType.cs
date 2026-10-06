using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document types accepted as a sole trader's proof of registration (EEA Sole Trader Full
    /// (3.0)).
    /// </summary>
    public enum ProofOfRegistrationType
    {
        [EnumMember(Value = "extract_from_trade_register")]
        ExtractFromTradeRegister,
        [EnumMember(Value = "other")]
        Other
    }
}