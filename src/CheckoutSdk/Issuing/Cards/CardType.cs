using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards
{
    public enum CardType
    {
        [EnumMember(Value = "virtual")] Virtual,
        [EnumMember(Value = "physical")] Physical
    }
}