using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.MerchantRiskInfo
{
    public enum ReorderItemsIndicatorType
    {
        [EnumMember(Value = "first_time_ordered")]
        FirstTimeOrdered,

        [EnumMember(Value = "reordered")]
        Reordered,
    }
}