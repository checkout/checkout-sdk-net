using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.GoogleSpa
{
    public enum GoogleSpaType
    {
        [EnumMember(Value = "Hosted")]
        Hosted,

        [EnumMember(Value = "Non Hosted")]
        NonHosted,
    }
}