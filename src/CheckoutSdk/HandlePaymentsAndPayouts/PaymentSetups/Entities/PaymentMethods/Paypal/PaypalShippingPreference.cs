using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Where to obtain the shipping information.
    /// </summary>
    public enum PaypalShippingPreference
    {
        /// <summary>
        /// Redacts the shipping address from the PayPal pages.
        /// </summary>
        [EnumMember(Value = "no_shipping")]
        NoShipping,

        /// <summary>
        /// Obtains the shipping address from the customer's PayPal account.
        /// </summary>
        [EnumMember(Value = "get_from_file")]
        GetFromFile,

        /// <summary>
        /// Uses the shipping address provided by the merchant.
        /// </summary>
        [EnumMember(Value = "set_provided_address")]
        SetProvidedAddress
    }
}
