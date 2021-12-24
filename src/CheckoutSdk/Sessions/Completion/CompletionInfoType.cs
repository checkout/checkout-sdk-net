using System.Runtime.Serialization;

namespace Checkout.Sessions.Completion
{
    public enum CompletionInfoType
    {
        [EnumMember(Value = "hosted")] Hosted,
        [EnumMember(Value = "non_hosted")] NonHosted
    }
}