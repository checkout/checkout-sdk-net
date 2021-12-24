using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionInterface
    {
        [EnumMember(Value = "native_ui")] NativeUi,
        [EnumMember(Value = "html")] Html
    }
}