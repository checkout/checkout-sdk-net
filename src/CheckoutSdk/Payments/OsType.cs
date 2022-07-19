using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum OsType
    {
        [EnumMember(Value = "ANDROID")] Android,
        [EnumMember(Value = "IOS")] Ios,
    }
}