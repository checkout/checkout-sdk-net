using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The type of ACH bank account.
    /// </summary>
    public enum AchAccountType
    {
        [EnumMember(Value = "savings")]
        Savings,

        [EnumMember(Value = "checking")]
        Checking,
    }
}
