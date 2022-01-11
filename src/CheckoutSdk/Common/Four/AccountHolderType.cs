using System.Runtime.Serialization;

namespace Checkout.Common.Four
{
    public enum AccountHolderType
    {
        [EnumMember(Value = "individual")] Individual,
        [EnumMember(Value = "corporate")] Corporate,
        [EnumMember(Value = "instrument")] Instrument
    }
}