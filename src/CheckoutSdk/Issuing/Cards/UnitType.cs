using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards
{
    public enum UnitType
    {
        [EnumMember(Value = "months")] Months,
        [EnumMember(Value = "years")] Years
    }
}