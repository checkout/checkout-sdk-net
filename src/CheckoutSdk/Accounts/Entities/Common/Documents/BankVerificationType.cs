using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document type accepted as bank verification.
    /// </summary>
    public enum BankVerificationType
    {
        [EnumMember(Value = "bank_statement")]
        BankStatement
    }
}