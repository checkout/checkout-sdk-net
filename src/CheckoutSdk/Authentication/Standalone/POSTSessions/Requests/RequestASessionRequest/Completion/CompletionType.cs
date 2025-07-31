using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion
{
    public enum CompletionType
    {
        [EnumMember(Value = "hosted")]
        Hosted,

        [EnumMember(Value = "non_hosted")]
        NonHosted,
    }
}