using System.Runtime.Serialization;

namespace Checkout.Issuing.Common
{
    public enum IssuingCardType
    {
        [EnumMember(Value = "virtual")] Virtual,
        [EnumMember(Value = "physical")] Physical
    }
}