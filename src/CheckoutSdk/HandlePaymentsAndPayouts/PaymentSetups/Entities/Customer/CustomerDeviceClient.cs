using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of client the customer uses to initiate the payment.
    /// </summary>
    public enum CustomerDeviceClient
    {
        /// <summary>
        /// A web browser on a desktop device.
        /// </summary>
        [EnumMember(Value = "web")]
        Web,

        /// <summary>
        /// A web browser on a mobile device.
        /// </summary>
        [EnumMember(Value = "mobile_web")]
        MobileWeb,

        /// <summary>
        /// A native mobile application.
        /// </summary>
        [EnumMember(Value = "app")]
        App
    }
}
