using System.Runtime.Serialization;

namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The type of digital card.
    /// </summary>
    public enum IssuingDigitalCardType
    {
        [EnumMember(Value = "secure_element")] SecureElement,
        [EnumMember(Value = "host_card_emulation")] HostCardEmulation,
        [EnumMember(Value = "card_on_file")] CardOnFile,
        [EnumMember(Value = "e_commerce")] ECommerce,
        [EnumMember(Value = "qr_code")] QrCode,
    }
}
