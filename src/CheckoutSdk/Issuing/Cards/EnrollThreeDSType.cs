using System.Runtime.Serialization;

namespace Checkout.Issuing.Cards
{
    public enum EnrollThreeDSType
    {
        [EnumMember(Value = "security_question")]
        SecurityQuestion,
        [EnumMember(Value = "Password")] Password
    }
}