using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    public enum PaypalShippingPreference
    {
        [EnumMember(Value = "no_shipping")]
        NoShipping,
        [EnumMember(Value = "get_from_file")]
        GetFromFile,
        [EnumMember(Value = "set_provided_address")]
        SetProvidedAddress
    }
}
