using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum AmountVariabilityType
    {
        [EnumMember(Value = "Fixed")]
        Fixed,

        [EnumMember(Value = "Variable")]
        Variable
    }
}