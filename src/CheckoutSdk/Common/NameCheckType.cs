using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum NameCheckType
    {
        [EnumMember(Value = "full_match")] FullMatch,
        [EnumMember(Value = "partial_match")] PartialMatch,
        [EnumMember(Value = "no_match")] NoMatch
    }
}