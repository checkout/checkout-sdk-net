using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum AccountNameInquiryType
    {
        [EnumMember(Value = "full_match")] FullMatch,
        [EnumMember(Value = "partial_match")] PartialMatch,
        [EnumMember(Value = "no_match")] NoMatch,
        [EnumMember(Value = "not_performed")] NotPerformed,
        [EnumMember(Value = "not_supported")] NotSupported,
    }
}