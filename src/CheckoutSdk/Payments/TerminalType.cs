using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum TerminalType
    {
        [EnumMember(Value = "APP")] App,
        [EnumMember(Value = "WAP")] Wap,
        [EnumMember(Value = "WEB")] Web,
    }
}