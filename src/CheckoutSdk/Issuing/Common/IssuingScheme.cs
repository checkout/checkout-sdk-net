using System.Runtime.Serialization;

namespace Checkout.Issuing.Common
{
    /// <summary>
    /// The card scheme.
    /// </summary>
    public enum IssuingScheme
    {
        [EnumMember(Value = "mastercard")] Mastercard,
        [EnumMember(Value = "visa")] Visa
    }
}
