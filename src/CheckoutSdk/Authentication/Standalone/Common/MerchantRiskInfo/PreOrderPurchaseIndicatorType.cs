using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.MerchantRiskInfo
{
    public enum PreOrderPurchaseIndicatorType
    {
        [EnumMember(Value = "merchandise_available")]
        MerchandiseAvailable,

        [EnumMember(Value = "future_availability")]
        FutureAvailability,
    }
}