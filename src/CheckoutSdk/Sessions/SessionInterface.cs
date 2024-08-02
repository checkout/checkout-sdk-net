using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionInterface
    {
        [EnumMember(Value = "html")] Html,
        [EnumMember(Value = "native_ui")] NativeUi
    }
}