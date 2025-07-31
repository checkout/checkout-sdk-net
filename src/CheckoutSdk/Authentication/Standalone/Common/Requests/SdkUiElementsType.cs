using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Requests
{
    public enum SdkUiElementsType
    {
        [EnumMember(Value = "text")]
        Text,

        [EnumMember(Value = "single_select")]
        SingleSelect,

        [EnumMember(Value = "multi_select")]
        MultiSelect,

        [EnumMember(Value = "oob")]
        Oob,

        [EnumMember(Value = "html_other")]
        HtmlOther,
    }
}