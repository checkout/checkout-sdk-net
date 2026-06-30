using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of account holder.
    /// </summary>
    public enum BacsAccountHolderType
    {
        [EnumMember(Value = "individual")] Individual,

        [EnumMember(Value = "corporate")] Corporate
    }
}
