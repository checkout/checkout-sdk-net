using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The type of SEPA mandate.
    /// </summary>
    public enum SepaMandateType
    {
        [EnumMember(Value = "Core")]
        Core,

        [EnumMember(Value = "B2B")]
        B2B,
    }
}
