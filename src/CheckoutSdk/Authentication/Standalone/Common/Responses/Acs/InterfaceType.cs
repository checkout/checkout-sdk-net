using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.Acs
{
    public enum InterfaceType
    {
        [EnumMember(Value = "native_ui")]
        NativeUi,

        [EnumMember(Value = "html")]
        Html,
    }
}