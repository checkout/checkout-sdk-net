using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum ShippingPreference
    {
        [EnumMember(Value = "NO_SHIPPING")] NoShipping,
        [EnumMember(Value = "SET_PROVIDED_ADDRESS")] SetProvidedAddress,
        [EnumMember(Value = "ANDROID")] GetFromFile,
    }
}