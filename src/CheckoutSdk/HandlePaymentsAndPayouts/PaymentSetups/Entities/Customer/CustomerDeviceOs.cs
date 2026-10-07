using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The operating system of the customer's device.
    /// </summary>
    public enum CustomerDeviceOs
    {
        /// <summary>
        /// Android.
        /// </summary>
        [EnumMember(Value = "android")]
        Android,

        /// <summary>
        /// iOS.
        /// </summary>
        [EnumMember(Value = "ios")]
        Ios
    }
}
