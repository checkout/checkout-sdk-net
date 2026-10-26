using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of Direct Debit account.
    /// </summary>
    public enum PaymentSetupAchAccountType
    {
        [EnumMember(Value = "savings")]
        Savings,

        [EnumMember(Value = "current")]
        Current,

        [EnumMember(Value = "cash")]
        Cash
    }
}
