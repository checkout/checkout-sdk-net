using System.Runtime.Serialization;

namespace Checkout.Instruments.Create
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
