using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardCategory
    {
        [EnumMember(Value = "commercial")]
        Commercial,

        [EnumMember(Value = "consumer")]
        Consumer,

        /// <summary>
        /// Returned only on card payout destinations.
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown
    }
}
