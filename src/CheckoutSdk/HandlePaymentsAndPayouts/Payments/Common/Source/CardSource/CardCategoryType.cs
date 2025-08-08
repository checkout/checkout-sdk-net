using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    CardSource
{
    public enum CardCategoryType
    {
        [EnumMember(Value = "CONSUMER")]
        CONSUMER,

        [EnumMember(Value = "COMMERCIAL")]
        COMMERCIAL,
    }
}