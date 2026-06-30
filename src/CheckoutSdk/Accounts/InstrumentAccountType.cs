using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    /// <summary>
    /// The type of bank account.
    /// </summary>
    public enum InstrumentAccountType
    {
        [EnumMember(Value = "savings")] Savings,

        [EnumMember(Value = "checking")] Checking
    }
}
